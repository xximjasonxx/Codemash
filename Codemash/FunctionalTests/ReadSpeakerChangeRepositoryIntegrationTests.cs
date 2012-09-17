using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Transactions;
using Codemash.Api.Data.Repositories;
using Codemash.Poller.Container;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace FunctionalTests
{
    [TestClass]
    public class ReadSpeakerChangeRepositoryIntegrationTests
    {
        private TransactionScope _transactionScope;

        [TestInitialize]
        public void Initialize()
        {
            _transactionScope = new TransactionScope();
            CreateSpeakerChangeTestData();
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_speakerchange_records_can_be_retrieved_with_general_getall()
        {
            // arrange
            var repository = new PollerContainer().Get<ISpeakerChangeRepository>();

            // act
            var list = repository.GetAll();

            // assert
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_calling_getall_with_a_matching_condition_returns_non_empty_list_of_matching_changes()
        {
            // arrange
            var repository = new PollerContainer().Get<ISpeakerChangeRepository>();

            // act
            var list = repository.GetAll(sc => sc.Key == "FirstName");

            // assert
            Assert.AreNotEqual(0, list.Count);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _transactionScope.Dispose();
        }

        // private method helpers
        public void CreateSpeakerChangeTestData()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString))
            {
                connection.Open();
                const string cmdText = "insert into SpeakerChanges(SpeakerId, DateCreated, Action, [Key], Value) values(@SpeakerId, @Created, 1, @Key, @Value)";
                using (var command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("@SpeakerId", 1);
                    command.Parameters.AddWithValue("@Created", DateTime.Now);
                    command.Parameters.AddWithValue("@Key", "FirstName");
                    command.Parameters.AddWithValue("@Value", "Sam");
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@SpeakerId", 2);
                    command.Parameters.AddWithValue("@Created", DateTime.Now);
                    command.Parameters.AddWithValue("@Key", "FirstName");
                    command.Parameters.AddWithValue("@Value", "Sean");
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@SpeakerId", 3);
                    command.Parameters.AddWithValue("@Created", DateTime.Now);
                    command.Parameters.AddWithValue("@Key", "FirstName");
                    command.Parameters.AddWithValue("@Value", "John");
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }
    }
}
