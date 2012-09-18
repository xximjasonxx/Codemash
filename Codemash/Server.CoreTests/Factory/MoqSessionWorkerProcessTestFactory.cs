using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Codemash.Api.Data;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Extensions;
using Codemash.Api.Data.Parsing.Impl;
using Codemash.Api.Data.Provider;
using Codemash.Api.Data.Repositories;
using Codemash.Server.Core.Extensions;
using Moq;
using Newtonsoft.Json.Linq;

namespace Server.CoreTests.Factory
{
    public static class MoqSessionWorkerProcessTestFactory
    {
        private static IList<Session> _sessionRepository;
        private static IList<SessionChange> _sessionChangeRepository;

        public static ISessionRepository GetStandardSessionRepository()
        {
            var mock = new Mock<ISessionRepository>();
            mock.Setup(m => m.GetAll()).Returns(GetStandardSessions);

            return mock.Object;
        }

        public static ISessionRepository GetNonStandardSessionRepository()
        {
            var mock = new Mock<ISessionRepository>();
            mock.Setup(m => m.GetAll()).Returns(() =>
                {
                    var sessionList = GetStandardSessions();
                    var rand = new Random(DateTime.Now.Second);
                    var randomNumber = rand.Next(0, sessionList.Count - 1);

                    sessionList[randomNumber].Title = "This is a Test";
                    sessionList[randomNumber].Start = sessionList[randomNumber].Start.AddHours(-1);
                    sessionList[randomNumber].Abstract = "My new abstract";

                    randomNumber = rand.Next(0, sessionList.Count - 1);
                    sessionList[randomNumber].Title = "This is another test";
                    sessionList[randomNumber].End = sessionList[randomNumber].End.AddHours(1);

                    return sessionList;
                });

            return mock.Object;
        }

        public static IMasterDataProvider GetStandardMasterDataProvider()
        {
            var mock = new Mock<IMasterDataProvider>();
            mock.Setup(m => m.GetAllSessions()).Returns(GetStandardSessions);

            return mock.Object;
        }

        public static ISessionChangeRepository GetSessionChangeRepository()
        {
            var mock = new Mock<ISessionChangeRepository>();
            mock.Setup(m => m.GetAll()).Returns(() => _sessionChangeRepository ?? (_sessionChangeRepository = new List<SessionChange>()));
            mock.Setup(m => m.SaveRange(It.IsAny<IEnumerable<SessionChange>>())).Callback(
                (IEnumerable<SessionChange> sessions) =>
                    {
                        if (_sessionChangeRepository == null)
                            _sessionChangeRepository = new List<SessionChange>();

                        sessions.ToList().ForEach(s => _sessionChangeRepository.Add(s));
                    });

            return mock.Object;
        }

        private static IList<Session> GetStandardSessions()
        {
            using (var reader = new StreamReader("Factory/StandardSession.json"))
            {
                var jsonString = reader.ReadToEnd();
                var array = JArray.Parse(jsonString);
                var trackParser = new TrackParse();
                var roomParser = new RoomParse();

                return (from it in array.AsJEnumerable()
                        select new Session
                                   {
                                       SessionId = it["SessionId"].ToString().AsInt(),
                                       Title = it["Title"].ToString(),
                                       Abstract = it["Abstract"].ToString(),
                                       LevelType = it["Level"].ToString().AsLevel(Level.Unknown),
                                       TrackType = trackParser.Parse(it["Track"].ToString(), Track.Unknown),
                                       RoomType = roomParser.Parse(it["Room"].ToString(), Room.Unknown),
                                       Start = it["StartTime"].ToString().AsDateTime(),
                                       End = it["EndTime"].ToString().AsDateTime(),
                                       SpeakerId = it["Speaker"]["SpeakerId"].ToString().AsInt()
                                   }).ToList();
            }
        }

        public static ISessionRepository GetOneLessSessionRepository()
        {
            var mock = new Mock<ISessionRepository>();
            _sessionRepository = null;

            mock.Setup(m => m.GetAll()).Returns(() =>
                                                    {
                                                        if (_sessionRepository == null)
                                                        {
                                                            _sessionRepository = GetStandardSessions();
                                                            _sessionRepository.RemoveAt(0);
                                                        }

                                                        return _sessionRepository;
                                                    });

            mock.Setup(m => m.SaveRange(It.IsAny<IEnumerable<Session>>()))
                .Callback((IEnumerable<Session> sessions) =>
                              {
                                  if (_sessionRepository == null)
                                      _sessionRepository = new List<Session>();

                                  _sessionRepository.Clear();
                                  sessions.ToList().ForEach(s => _sessionRepository.Add(s));
                              });

            return mock.Object;
        }

        public static ISessionRepository GetSessionRepositoryWithAddApplyRange()
        {
            var mock = new Mock<ISessionRepository>();
            _sessionRepository = null;

            mock.Setup(m => m.GetAll()).Returns(() =>
            {
                if (_sessionRepository == null)
                {
                    _sessionRepository = GetStandardSessions();
                }

                return _sessionRepository;
            });

            mock.Setup(m => m.SaveRange(It.IsAny<IEnumerable<Session>>()))
                .Callback((IEnumerable<Session> sessions) =>
                {
                    if (_sessionRepository == null)
                        _sessionRepository = new List<Session>();

                    _sessionRepository.Clear();
                    sessions.ToList().ForEach(s => _sessionRepository.Add(s));
                });

            return mock.Object;
        }

        public static IMasterDataProvider GetOneLessMasterDataProvider()
        {
            var mock = new Mock<IMasterDataProvider>();
            mock.Setup(m => m.GetAllSessions()).Returns(() =>
                                                            {
                                                                var sessionList = GetStandardSessions();
                                                                sessionList.RemoveAt(0);

                                                                return sessionList;
                                                            });

            return mock.Object;
        }

        public static ISessionRepository GetEmptySessionRepository()
        {
            var mock = new Mock<ISessionRepository>();
            _sessionRepository = new List<Session>();

            mock.Setup(m => m.GetAll()).Returns(() => _sessionRepository);
            mock.Setup(m => m.SaveRange(It.IsAny<IEnumerable<Session>>()))
                .Callback((IEnumerable<Session> sessions) =>
                {
                    if (_sessionRepository == null)
                        _sessionRepository = new List<Session>();

                    _sessionRepository.Clear();
                    sessions.ToList().ForEach(s => _sessionRepository.Add(s));
                });

            return mock.Object;
        }
    }
}
