using System;
using System.Collections.Generic;
using System.Linq;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Ex;
using Codemash.Api.Data.Extensions;
using Codemash.Api.Data.Repositories;
using Codemash.Server.Core.Extensions;
using Moq;

namespace Server.CoreTests.Factory
{
    public static class MoqSessionRepositoryFactory
    {
        private static IList<Session> _sessionRepository;

        public static ISessionRepository GetSessionRepositoryMock()
        {
            var mock = new Mock<ISessionRepository>();

            // define the mocked load method
            mock.Setup(m => m.Get(It.IsAny<int>())).Returns((int inValue) =>
                {
                    if (_sessionRepository.Count == 0)
                        throw new RepositoryNotLoadedException("Session");

                    return _sessionRepository.FirstOrDefault(s => s.SessionId == inValue);
                });

            mock.Setup(m => m.Get(It.IsAny<Func<Session, bool>>())).Returns<Func<Session, bool>>((predicate) =>
                {
                    if (_sessionRepository.Count == 0)
                        throw new RepositoryNotLoadedException("Session");

                    return _sessionRepository.FirstOrDefault(predicate);
                });

            mock.Setup(m => m.GetAll(It.IsAny<Func<Session, bool>>())).Returns<Func<Session, bool>>((predicate) =>
                {
                    if (_sessionRepository.Count == 0)
                        throw new RepositoryNotLoadedException("Session");

                    return _sessionRepository.Where(predicate).ToList();
                });

            mock.Setup(m => m.GetAll()).Returns(() =>
            {
                if (_sessionRepository.Count == 0)
                    throw new RepositoryNotLoadedException("Session");

                return _sessionRepository;
            });

            mock.Setup(m => m.Clear()).Callback(() => _sessionRepository.Clear());

            return mock.Object;
        }
    }
}
