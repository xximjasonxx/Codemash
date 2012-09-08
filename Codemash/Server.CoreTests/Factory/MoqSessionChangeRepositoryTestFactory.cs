using System.Collections.Generic;
using System.Linq;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Ex;
using Codemash.Api.Data.Repositories;
using Codemash.Server.Core.Extensions;
using Moq;

namespace Server.CoreTests.Factory
{
    public static class MoqSessionChangeRepositoryTestFactory
    {
        private static IList<SessionChange> _sessionChangeRepository;

        public static ISessionChangeRepository GetSessionChangeRepository()
        {
            var mockedRepository = new Mock<ISessionChangeRepository>();
            mockedRepository.Setup(m => m.Load()).Callback(() =>
                {
                    _sessionChangeRepository = new List<SessionChange>
                        {
                            new SessionChange()
                        };

                    _sessionChangeRepository.Apply(sc => sc.MarkAsExisting());
                });

            mockedRepository.Setup(m => m.GetAll()).Returns(() =>
                {
                    if (_sessionChangeRepository == null || _sessionChangeRepository.Count == 0)
                        throw new RepositoryNotLoadedException("SessionChange");

                    return _sessionChangeRepository;
                });

            mockedRepository.Setup(m => m.AddRange(It.IsAny<IEnumerable<SessionChange>>()))
                .Callback((IEnumerable<SessionChange> changesToAdd) => changesToAdd.ToList()
                    .ForEach(sc => _sessionChangeRepository.Add(sc)));

            mockedRepository.Setup(m => m.Clear()).Callback(() => _sessionChangeRepository.Clear());

            mockedRepository.Setup(m => m.Save()).Callback(() => _sessionChangeRepository.Apply(m => m.MarkAsExisting()));

            return mockedRepository.Object;
        }
    }
}
