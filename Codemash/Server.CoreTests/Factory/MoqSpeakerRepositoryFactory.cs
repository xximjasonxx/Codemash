using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Ex;
using Codemash.Api.Data.Repositories;
using Moq;

namespace Server.CoreTests.Factory
{
    public static class MoqSpeakerRepositoryFactory
    {
        private static IList<Speaker> _speakerRepository; 

        public static ISpeakerRepository GetSpeakerRepositoryMock()
        {
            var mock = new Mock<ISpeakerRepository>();
            mock.Setup(m => m.Load()).Callback(() =>
                {
                    _speakerRepository = new List<Speaker>
                        {
                            new Speaker { SpeakerId = 113, FirstName = "Sven Aelterman" },
                            new Speaker { SpeakerId = 130, FirstName = "Gregory Beamer" },
                            new Speaker { SpeakerId = 135, FirstName = "Sergey Barskiy" },
                            new Speaker { SpeakerId = 84, FirstName = "Steve Bodnar" }
                        };
                });

            mock.Setup(m => m.Get(It.IsAny<int>())).Returns((int id) =>
                {
                    if (_speakerRepository.Count == 0)
                        throw new RepositoryNotLoadedException("Speaker");

                    return _speakerRepository.FirstOrDefault(s => s.SpeakerId == id);
                });

            mock.Setup(m => m.Get(It.IsAny<Func<Speaker, bool>>())).Returns((Func<Speaker, bool> predicate) =>
                {
                    if (_speakerRepository.Count == 0)
                        throw new RepositoryNotLoadedException("Speaker");

                    return _speakerRepository.FirstOrDefault(predicate);
                });

            mock.Setup(m => m.GetAll()).Returns(() =>
                {
                    if (_speakerRepository.Count == 0)
                        throw new RepositoryNotLoadedException("Speaker");

                    return _speakerRepository;
                });

            mock.Setup(m => m.GetAll(It.IsAny<Func<Speaker, bool>>())).Returns((Func<Speaker, bool> predicate) =>
                {
                    if (_speakerRepository.Count == 0)
                        throw new RepositoryNotLoadedException("Speaker");

                    return _speakerRepository.Where(predicate).ToList();
                });

            mock.Setup(m => m.Clear()).Callback(() => _speakerRepository.Clear());

            return mock.Object;
        }
    }
}
