using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Http.Controllers;
using Codemash.DeltaApi.Controllers;
using Codemash.DeltaApi.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ninject.Parameters;

namespace DeltaApi.Web.Tests.Integration
{
    [TestClass]
    public class SessionChangeControllerIntegrationTests
    {
        private TransactionScope _transactionScope;
        private IKernel _container;

        [TestInitialize]
        public void Initialize()
        {
            // start the transaction
            _transactionScope = new TransactionScope();

            // create the container
            _container = new WebContainer();

            // create the test data
            CreateStandardTestData();
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_given_a_repository_with_changesets_calling_get_will_return_a_non_empty_list_with_all_change_sets()
        {
            // arrange
            var controller = (SessionChangeController)_container.Get<IHttpController>("SessionChange", new IParameter[0]);

            // act
            var result = controller.Get();

            // assert
            Assert.AreNotEqual(0, result.Count());
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_given_a_repository_with_multiple_changesets_calling_getall_for_a_particular_version_will_return_all_changesets_for_that_version_in_a_non_empty_list()
        {
            // arrange
            var controller = (SessionChangeController)_container.Get<IHttpController>("SessionChange", new IParameter[0]);

            // act
            var result = controller.Get(1);

            // assert
            Assert.AreNotEqual(0, result.Count());
            Assert.AreEqual(1, result.Select(sc => sc.Version).Distinct().Count());
            Assert.AreEqual(1, result.First().Version);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _transactionScope.Dispose();
            _container.Dispose();
        }

        // private helper methods
        private static void CreateStandardTestData()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString))
            {
                connection.Open();

                var cmdText = "insert into SessionChanges(SessionId, Action, [Key], Value, Version) values(@SessionId, 1, 'Title', 'New Title', @Version)";
                using (var command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("@SessionId", 1);
                    command.Parameters.AddWithValue("@Version", 1);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@SessionId", 2);
                    command.Parameters.AddWithValue("@Version", 1);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@SessionId", 2);
                    command.Parameters.AddWithValue("@Version", 1);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@SessionId", 2);
                    command.Parameters.AddWithValue("@Version", 2);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }
    }
}
