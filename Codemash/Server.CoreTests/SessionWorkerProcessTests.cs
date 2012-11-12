using System.Linq;
using System.Collections.Generic;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Provider;
using Codemash.Api.Data.Repositories;
using Codemash.Poller.Container;
using Codemash.Poller.Process;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;
using Server.CoreTests.Factory;

namespace Server.CoreTests
{
    [TestClass]
    public class SessionWorkerProcessTests
    {
        [TestMethod]
        public void test_that_a_process_instance_can_be_extracted_from_poller_container()
        {
            var container = new PollerContainer();
            Assert.AreNotEqual(null, container.Get<SessionWorkerProcess>());
        }

        [TestMethod]
        public void test_that_process_execute_can_be_run()
        {
            var container = new StandardKernel();
            var masterMock = new Mock<IMasterDataProvider>();
            masterMock.Setup(m => m.GetAllSessions()).Returns(new List<Session>());
            container.Bind<IMasterDataProvider>().ToConstant(masterMock.Object);

            var sessionMock = new Mock<ISessionRepository>();
            sessionMock.Setup(m => m.GetAll()).Returns(new List<Session>());
            container.Bind<ISessionRepository>().ToConstant(sessionMock.Object);
            container.Bind<ISessionChangeRepository>().ToConstant(new Mock<ISessionChangeRepository>().Object);

            var process = container.Get<SessionWorkerProcess>();
            process.Execute();
        }

        [TestMethod]
        public void test_that_in_the_scenario_where_there_are_no_difference_between_master_and_local_the_sessionchange_repository_should_remain_empty()
        {
            var kernel = new StandardKernel();
            kernel.Bind<ISessionRepository>().ToConstant(new MoqSessionWorkerProcessTestFactory().GetStandardSessionRepository());
            kernel.Bind<IMasterDataProvider>().ToConstant(new MoqSessionWorkerProcessTestFactory().GetStandardMasterDataProvider());
            kernel.Bind<ISessionChangeRepository>().ToConstant(new MoqSessionWorkerProcessTestFactory().GetSessionChangeRepository()).InSingletonScope();

            var process = kernel.Get<SessionWorkerProcess>();
            process.Execute();

            var repository = kernel.Get<ISessionChangeRepository>();
            Assert.AreEqual(0, repository.GetAll().Count);
        }

        [TestMethod]
        public void test_that_in_the_scenario_where_there_are_differences_between_the_sessions_in_master_and_local_session_sessionchange_repository_should_not_be_empty()
        {
            var kernel = new StandardKernel();
            kernel.Bind<ISessionRepository>().ToConstant(new MoqSessionWorkerProcessTestFactory().GetNonStandardSessionRepository());
            kernel.Bind<IMasterDataProvider>().ToConstant(new MoqSessionWorkerProcessTestFactory().GetStandardMasterDataProvider());
            kernel.Bind<ISessionChangeRepository>().ToConstant(new MoqSessionWorkerProcessTestFactory().GetSessionChangeRepository()).InSingletonScope();

            var process = kernel.Get<SessionWorkerProcess>();
            process.Execute();

            var repository = kernel.Get<ISessionChangeRepository>();
            Assert.AreNotEqual(0, repository.GetAll().Count);
        }

        [TestMethod]
        public void test_that_if_a_session_exists_in_master_it_is_added_to_the_local_source()
        {
            var kernel = new StandardKernel();
            kernel.Bind<ISessionRepository>().ToConstant(new MoqSessionWorkerProcessTestFactory().GetOneLessSessionRepository()).InSingletonScope();
            kernel.Bind<IMasterDataProvider>().ToConstant(new MoqSessionWorkerProcessTestFactory().GetStandardMasterDataProvider());
            kernel.Bind<ISessionChangeRepository>().ToConstant(new Mock<ISessionChangeRepository>().Object);

            var process = kernel.Get<SessionWorkerProcess>();
            process.Execute();

            var repository = kernel.Get<ISessionRepository>();
            var provider = kernel.Get<IMasterDataProvider>();
            Assert.AreEqual(provider.GetAllSessions().Count, repository.GetAll().Count);
        }

        [TestMethod]
        public void test_that_if_a_session_does_not_exist_in_the_master_it_is_removed_from_the_local_repository()
        {
            var kernel = new StandardKernel();
            kernel.Bind<ISessionRepository>().ToConstant(new MoqSessionWorkerProcessTestFactory().GetSessionRepositoryWithAddApplyRange()).InSingletonScope();
            kernel.Bind<IMasterDataProvider>().ToConstant(new MoqSessionWorkerProcessTestFactory().GetOneLessMasterDataProvider());
            kernel.Bind<ISessionChangeRepository>().ToConstant(new Mock<ISessionChangeRepository>().Object);

            var process = kernel.Get<SessionWorkerProcess>();
            process.Execute();

            var repository = kernel.Get<ISessionRepository>();
            var provider = kernel.Get<IMasterDataProvider>();
            Assert.AreEqual(provider.GetAllSessions().Count, repository.GetAll().Count);
        }

        [TestMethod]
        public void test_that_if_the_local_repository_is_empty_no_differences_should_be_recoreded_during_execution()
        {
            // arrange
            var kernel = new StandardKernel();
            kernel.Bind<IMasterDataProvider>().ToConstant(new MoqSessionWorkerProcessTestFactory().GetStandardMasterDataProvider());
            kernel.Bind<ISessionRepository>().ToConstant(new MoqSessionWorkerProcessTestFactory().GetEmptySessionRepository()).InSingletonScope();
            kernel.Bind<ISessionChangeRepository>().ToConstant(new MoqSessionWorkerProcessTestFactory().GetSessionChangeRepository()).InSingletonScope();

            // act
            var process = kernel.Get<SessionWorkerProcess>();
            process.Execute();

            // assert
            Assert.AreNotEqual(0, kernel.Get<ISessionRepository>().GetAll().Count);
            Assert.AreEqual(0, kernel.Get<ISessionChangeRepository>().GetAll().Count);
        }
    }
}
