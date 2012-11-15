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
    public class SessionControllerIntegrationTests
    {
        private TransactionScope _transactionScope;
        private WebContainer _container;

        [TestInitialize]
        public void Initialize()
        {
            // start the transaction
            _transactionScope = new TransactionScope();

            // add some test data
            CreateSessionTestData();

            // instantiate the container
            _container = new WebContainer();
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_test_data_can_be_extracted_using_session_controller_general_get_method_through_a_non_empty_list()
        {
            // arrange
            var controller = (SessionController) _container.Get<IHttpController>("Session", new IParameter[0]);

            // act
            var result = controller.Get();

            // assert
            Assert.AreNotEqual(0, result.Count());
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_given_some_starting_data_a_session_can_be_extracted_from_the_repository_based_on_its_primary_key_value()
        {
            // arrange
            var controller = (SessionController) _container.Get<IHttpController>("Session", new IParameter[0]);

            // act
            var sessionId = _container.Get<ISessionRepository>().GetAll().First().SessionId;
            var session = controller.Get(sessionId);

            // assert
            Assert.IsNotNull(session);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [ExpectedException(typeof(HttpResponseException))]
        public void test_that_if_an_invalid_id_is_provided_that_a_404_response_is_returned()
        {
            // arrange
            var controller = (SessionController) _container.Get<IHttpController>("Session", new IParameter[0]);

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
        private void CreateSessionTestData()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString))
            {
                connection.Open();

                // add a speaker
                var cmdText = "select isnull(max(speakerId), 1) from  Speakers";
                int speakerId = 0;
                using (var speakerCommand = new SqlCommand(cmdText, connection))
                {
                    speakerId = speakerCommand.ExecuteScalar().ToString().AsInt();
                    speakerCommand.CommandText = "insert into Speakers(SpeakerId, Biography, EmailAddress, Name) Values(" + speakerId + 1 + ", 'a', 'b', 'c');";
                    speakerCommand.ExecuteNonQuery();

                    speakerCommand.CommandText = "select max(SpeakerId) from Speakers";
                    speakerId = speakerCommand.ExecuteScalar().ToString().AsInt();
                }

                // add the sessions
                cmdText = "select isnull(max(SessionId), 0) + 1 from Sessions";
                using (var sessionCommand = new SqlCommand(cmdText, connection))
                {
                    var startingSessionId = sessionCommand.ExecuteScalar().ToString().AsInt();
                    sessionCommand.CommandText = "insert into Sessions(SessionId, SpeakerId, Title, Abstract, Start, [End], Level, Room, Track) values(@SessionId, @SpeakerId, 'a', 'b', GETDATE(), GETDATE(), 1, 1, 1)";

                    // session 1
                    sessionCommand.Parameters.AddWithValue("@SpeakerId", speakerId);
                    sessionCommand.Parameters.AddWithValue("@SessionId", startingSessionId + 1);
                    sessionCommand.ExecuteNonQuery();
                    sessionCommand.Parameters.Clear();

                    // session 2
                    sessionCommand.Parameters.AddWithValue("@SpeakerId", speakerId);
                    sessionCommand.Parameters.AddWithValue("@SessionId", startingSessionId + 2);
                    sessionCommand.ExecuteNonQuery();
                    sessionCommand.Parameters.Clear();

                    // session 3
                    sessionCommand.Parameters.AddWithValue("@SpeakerId", speakerId);
                    sessionCommand.Parameters.AddWithValue("@SessionId", startingSessionId + 3);
                    sessionCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
