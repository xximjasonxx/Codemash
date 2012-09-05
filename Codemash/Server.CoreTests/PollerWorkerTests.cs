using Codemash.Api.Data.Repositories;
using Codemash.Poller;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;

namespace Server.CoreTests
{
    [TestClass]
    public class PollerWorkerTests
    {
        private IKernel _testKernel;

        [TestInitialize]
        public void Init()
        {
            // instantiate the kernel
            _testKernel = new StandardKernel();

            // mock up the IMasterRepository
            var mock = new Mock<IMasterRepository>();
            _testKernel.Bind<IMasterRepository>().ToConstant(mock.Object);

            // constant bindings
            _testKernel.Bind<PollerWorkerRole>().ToSelf();
        }

        [TestMethod]
        public void test_instance_of_worker_role_can_be_extracted_from_container()
        {
            var workerRole = _testKernel.TryGet<PollerWorkerRole>();
            Assert.AreNotEqual(null, workerRole);
        }

        [TestMethod]
        public void test_new_instance_of_worker_creates_non_null_container()
        {
            var workerRole = _testKernel.Get<PollerWorkerRole>();
            Assert.AreNotEqual(null, workerRole.MasterRepository);
        }
    }
}
