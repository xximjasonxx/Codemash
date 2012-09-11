using System.Linq;
using System.Collections.Generic;
using Codemash.Api.Data;
using Codemash.Api.Data.Compare;
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
    public class PollerWorkerProcessTests
    {
        [TestMethod]
        public void test_that_a_process_instance_can_be_extracted_from_poller_container()
        {
            var container = new PollerContainer();
            Assert.AreNotEqual(null, container.Get<PollerWorkerProcess>());
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

            container.Bind<ISpeakerRepository>().ToConstant(new Mock<ISpeakerRepository>().Object);
            container.Bind<SessionCompare>().ToConstant(new Mock<SessionCompare>().Object);
            container.Bind<ISessionChangeRepository>().ToConstant(new Mock<ISessionChangeRepository>().Object);

            var process = container.Get<PollerWorkerProcess>();
            process.Execute();
        }

        [TestMethod]
        public void test_that_in_the_scenario_where_there_are_no_difference_between_master_and_local_the_sessionchange_repository_should_remain_empty()
        {
            var kernel = new StandardKernel();
            kernel.Bind<ISessionRepository>().ToConstant(MoqPollerWorkerProcessTestFactory.GetStandardSessionRepository());
            kernel.Bind<IMasterDataProvider>().ToConstant(MoqPollerWorkerProcessTestFactory.GetStandardMasterDataProvider());
            kernel.Bind<ISpeakerRepository>().ToConstant(new Mock<ISpeakerRepository>().Object);
            kernel.Bind<SessionCompare>().ToConstant(new SessionCompare());
            kernel.Bind<ISessionChangeRepository>().ToConstant(MoqPollerWorkerProcessTestFactory.GetSessionChangeRepository()).InSingletonScope();

            var process = kernel.Get<PollerWorkerProcess>();
            process.Execute();

            var repository = kernel.Get<ISessionChangeRepository>();
            Assert.AreEqual(0, repository.GetAll().Count);
        }

        [TestMethod]
        public void test_that_in_the_scenario_where_there_are_differences_between_the_sessions_in_master_and_local_session_sessionchange_repository_should_not_be_empty()
        {
            var kernel = new StandardKernel();
            kernel.Bind<ISessionRepository>().ToConstant(MoqPollerWorkerProcessTestFactory.GetNonStandardSessionRepository());
            kernel.Bind<IMasterDataProvider>().ToConstant(MoqPollerWorkerProcessTestFactory.GetStandardMasterDataProvider());
            kernel.Bind<SessionCompare>().ToConstant(new SessionCompare());
            kernel.Bind<ISessionChangeRepository>().ToConstant(MoqPollerWorkerProcessTestFactory.GetSessionChangeRepository()).InSingletonScope();
            kernel.Bind<ISpeakerRepository>().ToConstant(new Mock<ISpeakerRepository>().Object);

            var process = kernel.Get<PollerWorkerProcess>();
            process.Execute();

            var repository = kernel.Get<ISessionChangeRepository>();
            Assert.AreNotEqual(0, repository.GetAll().Count);
        }

        [TestMethod]
        public void test_that_if_a_session_exists_in_master_it_is_added_to_the_local_source()
        {
            var kernel = new StandardKernel();
            kernel.Bind<ISessionRepository>().ToConstant(MoqPollerWorkerProcessTestFactory.GetOneLessSessionRepository()).InSingletonScope();
            kernel.Bind<IMasterDataProvider>().ToConstant(MoqPollerWorkerProcessTestFactory.GetStandardMasterDataProvider());
            kernel.Bind<SessionCompare>().ToConstant(new SessionCompare());
            kernel.Bind<ISessionChangeRepository>().ToConstant(new Mock<ISessionChangeRepository>().Object);
            kernel.Bind<ISpeakerRepository>().ToConstant(new Mock<ISpeakerRepository>().Object);

            var process = kernel.Get<PollerWorkerProcess>();
            process.Execute();

            var repository = kernel.Get<ISessionRepository>();
            var provider = kernel.Get<IMasterDataProvider>();
            Assert.AreEqual(provider.GetAllSessions().Count, repository.GetAll().Count);
        }

        [TestMethod]
        public void test_that_if_a_session_does_not_exist_in_the_master_it_is_removed_from_the_local_repository()
        {
            var kernel = new StandardKernel();
            kernel.Bind<ISessionRepository>().ToConstant(MoqPollerWorkerProcessTestFactory.GetSessionRepositoryWithAddApplyRange());
            kernel.Bind<IMasterDataProvider>().ToConstant(MoqPollerWorkerProcessTestFactory.GetOneLessMasterDataProvider());
            kernel.Bind<SessionCompare>().ToConstant(new SessionCompare());
            kernel.Bind<ISessionChangeRepository>().ToConstant(new Mock<ISessionChangeRepository>().Object);
            kernel.Bind<ISpeakerRepository>().ToConstant(new Mock<ISpeakerRepository>().Object);

            var process = kernel.Get<PollerWorkerProcess>();
            process.Execute();

            var repository = kernel.Get<ISessionRepository>();
            var provider = kernel.Get<IMasterDataProvider>();
            Assert.AreEqual(provider.GetAllSessions().Count, repository.GetAll().Count(s => s.CurrentState != EntityState.Removed));
        }
    }
}
