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
    public static class MoqPollerWorkerProcessTestFactory
    {
        private static IList<Session> _sessionRepository;
        private static IList<SessionChange> _sessionChangeRepository;

        public static ISessionRepository GetStandardSessionRepository()
        {
            var mock = new Mock<ISessionRepository>();
            mock.Setup(m => m.Load()).Callback(() =>
                {
                    _sessionRepository = GetStandardSessions();
                    _sessionRepository.Apply(s => s.MarkAsExisting());
                });

            mock.Setup(m => m.GetAll()).Returns(() => _sessionRepository);

            return mock.Object;
        }

        public static ISessionRepository GetNonStandardSessionRepository()
        {
            var mock = new Mock<ISessionRepository>();
            mock.Setup(m => m.Load()).Callback(() =>
                {
                    var sessions = GetStandardSessions();
                    var rand = new Random(DateTime.Now.Second);
                    var randomNumber = rand.Next(0, sessions.Count - 1);

                    sessions[randomNumber].Title = "This is a Test";
                    sessions[randomNumber].Start = sessions[randomNumber].Start.AddHours(-1);
                    sessions[randomNumber].Abstract = "My new abstract";

                    randomNumber = rand.Next(0, sessions.Count - 1);
                    sessions[randomNumber].Title = "This is another test";
                    sessions[randomNumber].End = sessions[randomNumber].End.AddHours(1);

                    _sessionRepository = sessions;
                    _sessionRepository.Apply(s => s.MarkAsExisting());
                });

            mock.Setup(m => m.GetAll()).Returns(() => _sessionRepository);

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
            mock.Setup(m => m.AddRange(It.IsAny<IEnumerable<SessionChange>>())).Callback((IEnumerable<SessionChange> changes) =>
                {
                    if (_sessionChangeRepository == null)
                        _sessionChangeRepository = new List<SessionChange>();

                    foreach (var sessionChange in changes)
                        _sessionChangeRepository.Add(sessionChange);
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
                            Level = it["Level"].ToString().AsLevel(Level.Unknown),
                            Track = trackParser.Parse(it["Track"].ToString(), Track.Unknown),
                            Room = roomParser.Parse(it["Room"].ToString(), Room.Unknown),
                            Start = it["StartTime"].ToString().AsDateTime(),
                            End = it["EndTime"].ToString().AsDateTime(),
                            SpeakerId = it["Speaker"]["SpeakerId"].ToString().AsInt()
                        }).ToList();
            }
        }
    }
}
