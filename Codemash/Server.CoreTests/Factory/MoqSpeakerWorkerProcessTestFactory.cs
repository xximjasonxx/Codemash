using System;
using System.Collections.Generic;
using System.Linq;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Provider;
using Codemash.Api.Data.Repositories;
using Moq;

namespace Server.CoreTests.Factory
{
    public static class MoqSpeakerWorkerProcessTestFactory
    {
        public static IMasterDataProvider GetMasterProviderWithGetSpeakersMocked()
        {
            var speakerList = MoqSpeakerDataFactory.GetSpeakersFromFile();
            var mockedProvider = new Mock<IMasterDataProvider>();
            mockedProvider.Setup(m => m.GetAllSpeakers()).Returns(speakerList);

            return mockedProvider.Object;
        }

        public static IMasterDataProvider GetMasterProviderWithRandomlyAlteredSpeaker()
        {
            var speakerList = MoqSpeakerDataFactory.GetSpeakersFromFile();
            var rand = new Random(DateTime.Now.Second);
            var next = rand.Next(0, speakerList.Count - 1);
            speakerList[next].FirstName = "My Updated First Name";

            var mockedProvider = new Mock<IMasterDataProvider>();
            mockedProvider.Setup(m => m.GetAllSpeakers()).Returns(speakerList);

            return mockedProvider.Object;
        }

        public static IMasterDataProvider GetMasterProviderWithOneLessSpeakerMocked()
        {
            var speakerList = MoqSpeakerDataFactory.GetSpeakersFromFile();
            speakerList.RemoveAt(0);

            var mockedProvider = new Mock<IMasterDataProvider>();
            mockedProvider.Setup(m => m.GetAllSpeakers()).Returns(speakerList);

            return mockedProvider.Object;
        }

        public static ISpeakerRepository GetSpeakerRepositoryWithGetAllMockedToReturnStandardSpeakers()
        {
            var speakerList = MoqSpeakerDataFactory.GetSpeakersFromFile();
            var mockedProvider = new Mock<ISpeakerRepository>();
            mockedProvider.Setup(m => m.GetAll()).Returns(speakerList);

            return mockedProvider.Object;
        }

        public static ISpeakerRepository GetSpeakerRepositoryWithSpeakerRemovedMock()
        {
            var speakerList = MoqSpeakerDataFactory.GetSpeakersFromFile();
            speakerList.RemoveAt(0);
            
            var mock = new Mock<ISpeakerRepository>();
            mock.Setup(m => m.GetAll()).Returns(speakerList);

            return mock.Object;
        }

        private static IList<SpeakerChange> _speakerChangeRepository; 

        public static ISpeakerChangeRepository GetStandardSpeakerChangeRepositoryMock()
        {
            _speakerChangeRepository = null;
            var mock = new Mock<ISpeakerChangeRepository>();
            mock.Setup(m => m.GetAll()).Returns(() =>
                                                    {
                                                        if (_speakerChangeRepository == null)
                                                            _speakerChangeRepository = new List<SpeakerChange>();

                                                        return _speakerChangeRepository;
                                                    });

            mock.Setup(m => m.SaveRange(It.IsAny<IEnumerable<SpeakerChange>>())).Callback(
                (IEnumerable<SpeakerChange> changes) =>
                    {
                        if (_speakerChangeRepository == null)
                            _speakerChangeRepository = new List<SpeakerChange>();

                        changes.ToList().ForEach(c => _speakerChangeRepository.Add(c));
                    });

            return mock.Object;
        }

        private static IList<Speaker> _speakerRepository;

        public static ISpeakerRepository GetEmptySpeakerRepository()
        {
            var mock = new Mock<ISpeakerRepository>();
            _speakerRepository = new List<Speaker>();

            mock.Setup(m => m.GetAll()).Returns(() => _speakerRepository);
            mock.Setup(m => m.SaveRange(It.IsAny<IEnumerable<Speaker>>()))
                .Callback((IEnumerable<Speaker> speakers) =>
                {
                    if (_speakerRepository == null)
                        _speakerRepository = new List<Speaker>();

                    _speakerRepository.Clear();
                    speakers.ToList().ForEach(s => _speakerRepository.Add(s));
                });

            return mock.Object;
        }
    }
}
