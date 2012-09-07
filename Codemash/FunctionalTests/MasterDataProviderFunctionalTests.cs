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

            var sessions = masterDataProvider.GetAllSessions();
            Assert.AreNotEqual(0, sessions.Count);
        }
    }
}
