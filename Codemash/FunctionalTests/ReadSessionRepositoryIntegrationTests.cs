using System.Transactions;
using Codemash.Api.Data.Repositories;
using Codemash.Poller.Container;
using FunctionalTests.Factory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace FunctionalTests
{
    [TestClass]
    public class ReadSessionRepositoryIntegrationTests
    {
        private TransactionScope _transactionScope;

        [TestInitialize]
        public void TestInitialize()
        {
            // start the transaction
            _transactionScope = new TransactionScope();

            // create some test data
            SessionTestDataFactory.CreateStandardTestData();
        }

        [TestMethod]
        public void test_that_session_data_can_retrieved_using_get_all()
        {
            // arrange
            var container = new PollerContainer();
            
            // act
            var list = container.Get<ISessionRepository>().GetAll();

            // assert
            Assert.AreNotEqual(0, list.Count);
        }

        [TestMethod]
        public void test_that_session_data_can_retrieved_with_a_filter()
        {
            // arrange
            var container = new PollerContainer();

            // act
            var list = container.Get<ISessionRepository>().GetAll(s => s.Abstract.Contains("LINQ"));

            // assert
            Assert.AreNotEqual(0, list.Count);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _transactionScope.Dispose();
        }
    }
}
