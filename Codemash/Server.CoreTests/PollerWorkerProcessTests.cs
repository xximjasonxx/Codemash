using System.Collections.Generic;
using Codemash.Api.Data.Compare;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Provider;
using Codemash.Api.Data.Repositories;
using Codemash.Poller.Container;
using Codemash.Poller.Process;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;

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
    }
}
