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
        public void test_that_an_instance_can_be_extracted_from_poller_container()
        {
            var container = new PollerContainer();
            Assert.AreNotEqual(null, container.TryGet<PollerWorkerProcess>());
        }

        [TestMethod]
        public void test_that_execute_can_be_run()
        {
            var container = new StandardKernel();
            container.Bind<IMasterDataProvider>().ToConstant(new Mock<IMasterDataProvider>().Object);
            container.Bind<ISessionRepository>().ToConstant(new Mock<ISessionRepository>().Object);
            container.Bind<ISpeakerRepository>().ToConstant(new Mock<ISpeakerRepository>().Object);

            var process = container.Get<PollerWorkerProcess>();
            process.Execute();
        }
    }
}
