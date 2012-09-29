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
    public class SpeakerChangeControllerIntegrationTests
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

            // create test data
            CreateStandardTestData();
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_calling_the_general_controller_get_all_speaker_change_changesets_are_returned()
        {
            // arrange
            var controller = (SpeakerChangeController)_container.Get<IHttpController>("SpeakerChange", new IParameter[0]);

            // act
            var result = controller.Get();

            // assert
            Assert.IsTrue(result.Count() >= 1);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_calling_get_for_a_specific_version_will_return_only_changesets_pretaining_to_that_version()
        {
            // arrange
            var controller = (SpeakerChangeController)_container.Get<IHttpController>("SpeakerChange", new IParameter[0]);

            // act
            var result = controller.Get(1);

            // assert
            Assert.AreNotEqual(0, result.Count());
            Assert.AreEqual(1, result.Select(sc => sc.Version).Distinct().Count());
        }

        [TestCleanup]
        public void Cleanup()
        {
            _transactionScope.Dispose();
        }

        // private methods
        public static void CreateStandardTestData()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString))
            {
                connection.Open();

                var cmdText = "insert into SpeakerChanges(SpeakerId, Version, Action, [Key], Value) values(@SpeakerId, @Version, 1, 'Title', 'My New Title')";
                using (var command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("@SpeakerId", 1);
                    command.Parameters.AddWithValue("@Version", 1);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@SpeakerId", 2);
                    command.Parameters.AddWithValue("@Version", 1);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@SpeakerId", 3);
                    command.Parameters.AddWithValue("@Version", 1);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@SpeakerId", 4);
                    command.Parameters.AddWithValue("@Version", 2);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }
    }
}
