using Codemash.Api.Data.Provider;
using Codemash.Api.Data.Repositories;
using Codemash.Poller.Container;
using Codemash.Poller.Process;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ninject.Parameters;

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
            Assert.AreNotEqual(null, TheContainer.TryGet<SessionWorkerProcess>());
        }

        [TestMethod]
        public void test_poller_container_properly_resolves_the_session_worker_process()
        {
            Assert.IsInstanceOfType(TheContainer.Get<IProcess>("Session", new IParameter[0]), typeof(SessionWorkerProcess));
        }

        [TestMethod]
        public void test_poller_container_properly_resolves_the_speaker_worker_process()
        {
            Assert.IsInstanceOfType(TheContainer.Get<IProcess>("Speaker", new IParameter[0]), typeof(SpeakerWorkerProcess));
        }

        [TestMethod]
        public void test_poller_container_can_properly_resolve_speaker_change_repository()
        {
            Assert.IsNotNull(TheContainer.TryGet<ISpeakerChangeRepository>());
        }
    }
}
