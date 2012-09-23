using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web.Http;
using System.Web.Http.Controllers;
using Codemash.Api.Data.Repositories;
using Codemash.DeltaApi.Controllers;
using Codemash.DeltaApi.Core;
using Codemash.Server.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ninject.Parameters;

namespace DeltaApi.Web.Tests.Integration
{
    [TestClass]
    public class SpeakerControllerIntegrationTests
    {
        private TransactionScope _transactionScope;
        private WebContainer _container;

        [TestInitialize]
        public void Initialize()
        {
            _transactionScope = new TransactionScope();
            _container = new WebContainer();
            InsertTestSpeakerData();
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_get_returns_non_empty_list()
        {
            // arrange
            var controller = (SpeakerController) _container.Get<IHttpController>("Speaker", new IParameter[0]);

            // act
            var result = controller.Get();

            // assert
            Assert.AreNotEqual(0, result.Count());
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_given_a_valid_speaker_primary_key_value_a_non_null_speaker_instance_is_returned()
        {
            // arrange
            var controller = (SpeakerController) _container.Get<IHttpController>("Speaker", new IParameter[0]);
            var speakerId = _container.Get<ISpeakerRepository>().GetAll()[0].SpeakerId;

            // act
            var result = controller.Get(speakerId);

            // arrange
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [ExpectedException(typeof(HttpResponseException))]
        public void test_that_if_given_an_invalid_primary_key_identifier_for_speaker_get_a_404_response_code_is_raised()
        {
            // arrange
            var controller = (SpeakerController) _container.Get<IHttpController>("Speaker", new IParameter[0]);

            // act
            try
            {
                controller.Get(int.MaxValue);
            }
            catch (HttpResponseException responseException)
            {
                // assert
                Assert.AreEqual(HttpStatusCode.NotFound, responseException.Response.StatusCode);
                throw;
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            _transactionScope.Dispose();
            _container.Dispose();
        }

        // private helper methods
        private void InsertTestSpeakerData()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString))
            {
                connection.Open();

                var commandText = "select max(speakerid) + 1 from speakers";
                using (var command = new SqlCommand(commandText, connection))
                {
                    int speakerId = command.ExecuteScalar().ToString().AsInt();
                    commandText = "insert into speakers(SpeakerId, Biography, EmailAddress, FirstName, LastName) values(@SpeakerId, 'a', 'b', 'c', 'd');";
                    command.CommandText = commandText;

                    command.Parameters.AddWithValue("@SpeakerId", speakerId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
