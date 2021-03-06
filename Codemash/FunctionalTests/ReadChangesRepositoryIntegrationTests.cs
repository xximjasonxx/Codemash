﻿using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Transactions;
using Codemash.Api.Data.Repositories;
using Codemash.Poller.Container;
using Codemash.Server.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace FunctionalTests
{
    [TestClass]
    public class ReadChangesRepositoryIntegrationTests
    {
        private TransactionScope _transactionScope;

        [TestInitialize]
        public void Initialize()
        {
            _transactionScope = new TransactionScope();

            CreateTestData();
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_sessionchange_instances_can_be_extracted_enmasse_from_the_database()
        {
            // arrange
            var repository = new PollerContainer().Get<IChangeRepository>();

            // act
            var changes = repository.GetAll();

            // assert
            Assert.AreNotEqual(0, changes.Count);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_given_a_matching_condition_applicable_sessionchange_instances_can_be_extracted()
        {
            // arrange
            var repository = new PollerContainer().Get<IChangeRepository>();

            // act
            var list = repository.GetAll(sc => sc.EntityId == 1);

            // assert
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_given_a_valid_primary_key_value_get_returns_a_non_null_sessionchange_instance()
        {
            // arrange
            var repository = new PollerContainer().Get<IChangeRepository>();

            // act
            var lastSessionChangeId = GetLastChangeId();
            var sessionChange = repository.Get(lastSessionChangeId);

            // assert
            Assert.IsNotNull(sessionChange);
            Assert.AreEqual(lastSessionChangeId, sessionChange.ChangeId);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_given_an_invalid_primary_key_value_get_returns_a_null_session_change_instance()
        {
            // arrange
            var repository = new PollerContainer().Get<IChangeRepository>();

            // act
            var sessionChange = repository.Get(int.MaxValue);

            // assert
            Assert.IsNull(sessionChange);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_given_a_matching_condition_get_returns_a_non_null_instance_of_sessionchange()
        {
            // arrange
            var repository = new PollerContainer().Get<IChangeRepository>();

            // act
            var sessionChange = repository.Get(sc => sc.Key == "Title" && sc.EntityId == 1);

            // assert
            Assert.IsNotNull(sessionChange);
            Assert.AreEqual(1, sessionChange.EntityId);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_given_a_non_matching_condition_get_returns_a_null_instance_of_sessionchange()
        {
            // arrange
            var repository = new PollerContainer().Get<IChangeRepository>();

            // act
            var sessionChange = repository.Get(sc => sc.Key == null);

            // assert
            Assert.IsNull(sessionChange);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _transactionScope.Dispose();
        }

        // private helper methods
        private void CreateTestData()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString))
            {
                connection.Open();
                const string commandText = "insert into Changes(Changeset, EntityId, EntityType, Action, [Key], Value) values(1, @SessionId, 'Session', 1, @Key, @Value)";
                using (var command = new SqlCommand(commandText, connection))
                {
                    // session change #1
                    command.Parameters.AddWithValue("@SessionId", 1);
                    command.Parameters.AddWithValue("@Key", "Title");
                    command.Parameters.AddWithValue("@Value", "My New Title 1");
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                    // session change #2
                    command.Parameters.AddWithValue("@SessionId", 2);
                    command.Parameters.AddWithValue("@Key", "Title");
                    command.Parameters.AddWithValue("@Value", "My New Title 2");
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                    // session change #3
                    command.Parameters.AddWithValue("@SessionId", 3);
                    command.Parameters.AddWithValue("@Key", "Title");
                    command.Parameters.AddWithValue("@Value", "My New Title 3");
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                    // session change #4
                    command.Parameters.AddWithValue("@SessionId", 4);
                    command.Parameters.AddWithValue("@Key", "Title");
                    command.Parameters.AddWithValue("@Value", "My New Title 4");
                    command.ExecuteNonQuery();
                }
            }
        }

        private int GetLastChangeId()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString))
            {
                connection.Open();
                const string cmdText = "select max(changeid) from changes";
                using (var command = new SqlCommand(cmdText, connection))
                {
                    return command.ExecuteScalar().ToString().AsInt();
                }
            }
        }
    }
}
