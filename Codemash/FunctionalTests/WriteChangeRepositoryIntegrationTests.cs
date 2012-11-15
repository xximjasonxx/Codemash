using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Codemash.Api.Data;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Repositories;
using Codemash.Poller.Container;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace FunctionalTests
{
    [TestClass]
    public class WriteChangeRepositoryIntegrationTests
    {
        private TransactionScope _transactionScope;

        [TestInitialize]
        public void Initialize()
        {
            _transactionScope = new TransactionScope();
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_saving_a_range_of_sessionchange_instances_allows_those_instances_to_be_retrievable()
        {
            // arrange
            var repository = new PollerContainer().Get<IChangeRepository>();

            // act
            repository.SaveRange(GetChangeTestData());

            // assert
            Assert.AreNotEqual(0, repository.GetAll().Count);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_saving_a_range_produces_one_distinct_timestamp_for_creation_grouping()
        {
            // arrange
            var repository = new PollerContainer().Get<IChangeRepository>();

            // act
            repository.SaveRange(GetChangeTestData());

            // assert
            var blocks = repository.GetAll().Select(sc => sc.Changeset).Distinct();
            Assert.AreNotEqual(0, blocks.Count());
            Assert.AreNotEqual(DateTime.MinValue, blocks.First());
        }

        [TestCleanup]
        public void Cleanup()
        {
            _transactionScope.Dispose();
        }

        // private helper methods
        private IEnumerable<Change> GetChangeTestData()
        {
            return new List<Change>
                       {
                           new Change { Action = ChangeAction.Modify.ToString(), EntityId = 1, Key = "Title", Value = "My New Title 1", EntityType = "Session" },
                           new Change { Action = ChangeAction.Modify.ToString(), EntityId = 2, Key = "Title", Value = "My New Title 2", EntityType = "Session" },
                           new Change { Action = ChangeAction.Modify.ToString(), EntityId = 3, Key = "Title", Value = "My New Title 3", EntityType = "Speaker" },
                           new Change { Action = ChangeAction.Modify.ToString(), EntityId = 4, Key = "Title", Value = "My New Title 4", EntityType = "Session" }
                       };
        }
    }
}
