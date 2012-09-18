using System.Linq;
using Codemash.Api.Data;
using Codemash.Api.Data.Provider;
using Codemash.Api.Data.Repositories;
using Codemash.Poller.Process;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Server.CoreTests.Factory;

namespace Server.CoreTests
{
    [TestClass]
    public class SpeakerWorkerProcessTests
    {
        private IKernel _theKernel;

        [TestInitialize]
        public void TestInitialize()
        {
            _theKernel = new StandardKernel();
            _theKernel.Bind<IProcess>().To<SpeakerWorkerProcess>();
            _theKernel.Bind<ISpeakerChangeRepository>().ToConstant(MoqSpeakerWorkerProcessTestFactory.GetStandardSpeakerChangeRepositoryMock()).InSingletonScope();
        }

        [TestMethod]
        public void test_when_no_changes_are_detected_between_master_and_local_no_differences_recorded_for_saving()
        {
            // arrange
            _theKernel.Bind<IMasterDataProvider>().ToConstant(MoqSpeakerWorkerProcessTestFactory.GetMasterProviderWithGetSpeakersMocked());
            _theKernel.Bind<ISpeakerRepository>().ToConstant(MoqSpeakerWorkerProcessTestFactory.GetSpeakerRepositoryWithGetAllMockedToReturnStandardSpeakers());

            // act
            var process = _theKernel.Get<IProcess>();
            process.Execute();

            // assert
            var repository = _theKernel.Get<ISpeakerChangeRepository>();
            Assert.AreEqual(0, repository.GetAll().Count);
        }

        [TestMethod]
        public void test_when_changes_exist_between_master_and_local_differences_are_recorded_in_speaker_change_repository()
        {
            // arrange
            _theKernel.Bind<IMasterDataProvider>().ToConstant(MoqSpeakerWorkerProcessTestFactory.GetMasterProviderWithRandomlyAlteredSpeaker());
            _theKernel.Bind<ISpeakerRepository>().ToConstant(MoqSpeakerWorkerProcessTestFactory.GetSpeakerRepositoryWithGetAllMockedToReturnStandardSpeakers());

            // act
            var process = _theKernel.Get<IProcess>();
            process.Execute();

            // assert
            var repository = _theKernel.Get<ISpeakerChangeRepository>();
            Assert.AreEqual(1, repository.GetAll().Count);
        }

        [TestMethod]
        public void test_when_a_session_exists_in_master_and_not_in_local_that_differences_should_be_recorded()
        {
            // arrange
            _theKernel.Bind<IMasterDataProvider>().ToConstant(MoqSpeakerWorkerProcessTestFactory.GetMasterProviderWithGetSpeakersMocked());
            _theKernel.Bind<ISpeakerRepository>().ToConstant(MoqSpeakerWorkerProcessTestFactory.GetSpeakerRepositoryWithSpeakerRemovedMock());

            // act
            var process = _theKernel.Get<IProcess>();
            process.Execute();

            // assert
            var repository = _theKernel.Get<ISpeakerChangeRepository>();
            Assert.AreNotEqual(0, repository.GetAll().Count(sc => sc.ActionType == ChangeAction.Add));
        }

        [TestMethod]
        public void test_when_a_session_is_removed_from_master_that_a_delete_difference_is_recorded()
        {
            // arrange
            _theKernel.Bind<IMasterDataProvider>().ToConstant(MoqSpeakerWorkerProcessTestFactory.GetMasterProviderWithOneLessSpeakerMocked());
            _theKernel.Bind<ISpeakerRepository>().ToConstant(MoqSpeakerWorkerProcessTestFactory.GetSpeakerRepositoryWithGetAllMockedToReturnStandardSpeakers());

            // act
            var process = _theKernel.Get<IProcess>();
            process.Execute();

            // assert
            var repository = _theKernel.Get<ISpeakerChangeRepository>();
            Assert.AreEqual(1, repository.GetAll().Count(sp => sp.ActionType == ChangeAction.Delete));
        }

        [TestMethod]
        public void test_that_in_the_case_that_no_speakers_are_stored_locally_no_differences_should_be_recorded_for_the_operation_execution()
        {
            // arrange;
            _theKernel.Bind<IMasterDataProvider>().ToConstant(MoqSpeakerWorkerProcessTestFactory.GetMasterProviderWithGetSpeakersMocked());
            _theKernel.Bind<ISpeakerRepository>().ToConstant(MoqSpeakerWorkerProcessTestFactory.GetEmptySpeakerRepository()).InSingletonScope();

            // act
            var process = _theKernel.Get<IProcess>();
            process.Execute();

            // assert
            Assert.AreNotEqual(0, _theKernel.Get<ISpeakerRepository>().GetAll().Count);
            Assert.AreEqual(0, _theKernel.Get<ISpeakerChangeRepository>().GetAll().Count);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _theKernel.Dispose();
        }
    }
}
