using System.Configuration;
using System.Data.SqlClient;
using System.Transactions;
using Codemash.Api.Data.Repositories;
using Codemash.Poller.Container;
using Codemash.Server.Core.Extensions;
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
        [TestCategory("Integration")]
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
        [TestCategory("Integration")]
        public void test_that_session_data_can_retrieved_with_a_filter()
        {
            // arrange
            var container = new PollerContainer();

            // act
            var list = container.Get<ISessionRepository>().GetAll(s => s.Abstract.Contains("LINQ"));

            // assert
            Assert.AreNotEqual(0, list.Count);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_requesting_a_session_using_get_with_a_valid_id_returns_the_appropriate_instance()
        {
            // arrange
            var container = new PollerContainer();
            int sessionId = int.MinValue;
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("select max(sessionId) from sessions", connection))
                {
                    sessionId = command.ExecuteScalar().ToString().AsInt();
                }
            }

            // act
            var session = container.Get<ISessionRepository>().Get(sessionId);

            // assert
            Assert.IsNotNull(session);
            Assert.AreEqual(sessionId, session.SessionId);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_giving_an_invalid_session_id_to_get_returns_a_null_session_reference()
        {
            // arrange
            var container = new PollerContainer();

            // act
            var session = container.Get<ISessionRepository>().Get(int.MaxValue);

            // assert
            Assert.IsNull(session);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_given_a_matching_condition_get_returns_a_non_null_session_references()
        {
            // arrange
            var container = new PollerContainer();

            // act
            var session = container.Get<ISessionRepository>().Get(s => s.Title != string.Empty);

            // assert
            Assert.IsNotNull(session);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_given_a_non_matching_condition_get_returns_a_null_session_reference()
        {
            // arrange
            var container = new PollerContainer();

            // act
            var session = container.Get<ISessionRepository>().Get(s => s.Title == null);

            // assert
            Assert.IsNull(session);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _transactionScope.Dispose();
        }
    }
}
