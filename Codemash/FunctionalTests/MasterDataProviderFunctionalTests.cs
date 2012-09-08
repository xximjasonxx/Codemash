using System;
using Codemash.Api.Data.Provider;
using Codemash.Poller.Container;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace FunctionalTests
{
    [TestClass]
    public class MasterDataProviderFunctionalTests
    {
        [TestMethod]
        public void test_that_sessions_can_be_downloaded_and_loaded()
        {
            var container = new PollerContainer();
            var masterDataProvider = container.TryGet<IMasterDataProvider>();
            Assert.AreNotEqual(null, masterDataProvider);

            var sessions = masterDataProvider.GetAllSessions();
            Assert.AreNotEqual(0, sessions.Count);
        }

        [TestMethod]
        public void test_that_speakers_can_be_downloaded_and_loaded()
        {
            var container = new PollerContainer();
            var masterDataProvider = container.TryGet<IMasterDataProvider>();
            Assert.AreNotEqual(null, masterDataProvider);

            var speakers = masterDataProvider.GetAllSpeakers();
            Assert.AreNotEqual(0, speakers.Count);
        }
    }
}
