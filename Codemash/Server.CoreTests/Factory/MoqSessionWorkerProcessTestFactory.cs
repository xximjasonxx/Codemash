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
using Ninject;
using Server.CoreTests.Containers;

namespace Server.CoreTests.Factory
{
    public class MoqSessionWorkerProcessTestFactory
    {
        private TestDataFactory _dataFactory;

        public MoqSessionWorkerProcessTestFactory()
        {
            var container = new TestContainer();
            _dataFactory = container.Get<TestDataFactory>();
        }

        private IList<Session> _sessionRepository;
        private IList<Change> _sessionChangeRepository;

        public ISessionRepository GetStandardSessionRepository()
        {
            var mock = new Mock<ISessionRepository>();
            mock.Setup(m => m.GetAll()).Returns(_dataFactory.GetSessions);

            return mock.Object;
        }

        public ISessionRepository GetNonStandardSessionRepository()
        {
            var mock = new Mock<ISessionRepository>();
            mock.Setup(m => m.GetAll()).Returns(() =>
                                                    {
                                                        var sessionList = _dataFactory.GetSessions();
                                                        var rand = new Random(DateTime.Now.Second);
                                                        var randomNumber = rand.Next(0, sessionList.Count - 1);

                                                        sessionList[randomNumber].Title = "This is a Test";
                                                        sessionList[randomNumber].Start = DateTime.Now.Date;
                                                        sessionList[randomNumber].Abstract = "My new abstract";

                                                        randomNumber = rand.Next(0, sessionList.Count - 1);
                                                        sessionList[randomNumber].Title = "This is another test";
                                                        sessionList[randomNumber].End = sessionList[randomNumber].End.AddHours(1);

                                                        return sessionList;
                                                    });

            return mock.Object;
        }

        public IMasterDataProvider GetStandardMasterDataProvider()
        {
            var mock = new Mock<IMasterDataProvider>();
            mock.Setup(m => m.GetAllSessions()).Returns(_dataFactory.GetSessions);

            return mock.Object;
        }

        public IChangeRepository GetSessionChangeRepository()
        {
            _sessionChangeRepository = null;
            var mock = new Mock<IChangeRepository>();
            mock.Setup(m => m.GetAll()).Returns(() => _sessionChangeRepository ?? (_sessionChangeRepository = new List<Change>()));
            mock.Setup(m => m.SaveRange(It.IsAny<IEnumerable<Change>>())).Callback(
                (IEnumerable<Change> sessions) =>
                    {
                        if (_sessionChangeRepository == null)
                            _sessionChangeRepository = new List<Change>();

                        sessions.ToList().ForEach(s => _sessionChangeRepository.Add(s));
                    });

            return mock.Object;
        }

        public ISessionRepository GetOneLessSessionRepository()
        {
            var mock = new Mock<ISessionRepository>();
            _sessionRepository = null;

            mock.Setup(m => m.GetAll()).Returns(() =>
                                                    {
                                                        if (_sessionRepository == null)
                                                        {
                                                            _sessionRepository = _dataFactory.GetSessions();
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

        public ISessionRepository GetSessionRepositoryWithAddApplyRange()
        {
            var mock = new Mock<ISessionRepository>();
            _sessionRepository = null;

            mock.Setup(m => m.GetAll()).Returns(() =>
            {
                if (_sessionRepository == null)
                {
                    _sessionRepository = _dataFactory.GetSessions();
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

        public IMasterDataProvider GetOneLessMasterDataProvider()
        {
            var mock = new Mock<IMasterDataProvider>();
            mock.Setup(m => m.GetAllSessions()).Returns(() =>
                                                            {
                                                                var sessionList = _dataFactory.GetSessions();
                                                                sessionList.RemoveAt(0);

                                                                return sessionList;
                                                            });

            return mock.Object;
        }

        public ISessionRepository GetEmptySessionRepository()
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
