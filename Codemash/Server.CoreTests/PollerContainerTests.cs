using Codemash.Api.Data.Provider;
using Codemash.Api.Data.Repositories;
using Codemash.Poller.Container;
using Codemash.Poller.Process;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace Server.CoreTests
{
    [TestClass]
    public class PollerContainerTests
    {
        public PollerContainer TheContainer;

        [TestInitialize]
        public void Init()
        {
            TheContainer = new PollerContainer();
        }

        [TestMethod]
        public void test_poller_provides_an_implementation_to_access_master_data_provider()
        {
            Assert.AreNotEqual(null, TheContainer.TryGet<IMasterDataProvider>());
        }

        [TestMethod]
        public void test_poller_provides_an_implementation_to_access_local_session_data()
        {
            Assert.AreNotEqual(null, TheContainer.TryGet<ISessionRepository>());
        }

        [TestMethod]
        public void test_poller_provides_an_implementation_to_access_local_speaker_data()
        {
            Assert.AreNotEqual(null, TheContainer.TryGet<ISpeakerRepository>());
        }

        [TestMethod]
        public void test_poller_container_can_retrieve_instance_of_poller_worker_process()
        {
            Assert.AreNotEqual(null, TheContainer.TryGet<PollerWorkerProcess>());
        }
    }
}
