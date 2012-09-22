using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using Codemash.Api.Data;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Repositories;
using Codemash.Poller.Container;
using FunctionalTests.Factory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace FunctionalTests
{
    [TestClass]
    public class WriteSessionRepositoryIntegrationTests
    {
        private TransactionScope _transactionScope;
        private int _speakerId;

        [TestInitialize]
        public void Initialize()
        {
            _transactionScope = new TransactionScope();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString))
            {
                connection.Open();
                SessionTestDataFactory.CreateStandardSpeaker(connection);
                _speakerId = SessionTestDataFactory.GetLastSpeakerId(connection);
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_given_a_range_of_sessions_those_sessions_are_saved_to_the_database_and_can_be_retrieved()
        {
            // arrange
            var sessions = GetTestSessionList();
            var container = new PollerContainer();
            var repository = container.Get<ISessionRepository>();

            // act
            repository.SaveRange(sessions);

            // assert
            Assert.IsTrue(sessions.Count >= repository.GetAll().Count);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_given_a_master_with_sessions_matching_existing_sessions_sessions_are_updated_with_new_values()
        {
            // arrange
            var sessions = GetTestSessionList();
            var originalSessionCount = sessions.Count;
            var container = new PollerContainer();
            var repository = container.Get<ISessionRepository>();

            // add test data
            repository.SaveRange(sessions);
            sessions = repository.GetAll();
            int sessionId = sessions[0].SessionId;
            
            // precondition
            Assert.AreNotEqual(0, sessionId);

            // act
            sessions[0].Title = "new title";
            repository.SaveRange(sessions);

            // assert
            Assert.AreEqual(originalSessionCount, repository.GetAll().Count);
            Assert.AreEqual("new title", repository.Get(sessionId).Title);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_given_a_session_range_which_does_not_include_a_session_currently_in_the_database_that_session_should_be_removed()
        {
            // arrange
            var sessions = GetTestSessionList();
            var container = new PollerContainer();
            var repository = container.Get<ISessionRepository>();

            // add
            repository.SaveRange(sessions);

            // act
            sessions = repository.GetAll();
            var removedSessionId = sessions[0].SessionId;
            sessions.RemoveAt(0);
            var sessionCount = sessions.Count;
            repository.SaveRange(sessions);

            // assert
            Assert.IsNull(repository.Get(removedSessionId));
            Assert.AreEqual(sessionCount, repository.GetAll().Count);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_given_a_master_list_containing_a_session_not_in_the_database_that_the_session_is_added_to_the_database()
        {
            // arrange
            var sessions = GetTestSessionList();
            var repository = new PollerContainer().Get<ISessionRepository>();

            // add
            var count = sessions.Count;
            repository.SaveRange(sessions.Take(sessions.Count - 1));

            // act
            sessions = repository.GetAll();
            var session = GetTestSessionList()[0];
            session.SessionId = 4;
            sessions.Add(session);
            repository.SaveRange(sessions);

            // assert
            Assert.AreEqual(count, repository.GetAll().Count);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _transactionScope.Dispose();
        }

        // private helper methods
        private IList<Session> GetTestSessionList()
        {
            return new List<Session>
                       {
                           new Session
                               {
                                   SessionId = 1,
                                   Title = "Session 1",
                                   Abstract = "Abstract",
                                   End = DateTime.Now.AddHours(1),
                                   LevelType = Level.General,
                                   RoomType = Room.CtrEFG,
                                   SpeakerId = _speakerId,
                                   Start = DateTime.Now
                               },
                           new Session
                               {
                                   SessionId = 2,
                                   Title = "Session 2",
                                   Abstract = "Abstract",
                                   End = DateTime.Now.AddHours(1),
                                   LevelType = Level.General,
                                   RoomType = Room.CtrEFG,
                                   SpeakerId = _speakerId,
                                   Start = DateTime.Now
                               },
                           new Session
                               {
                                   SessionId = 3,
                                   Title = "Session 3",
                                   Abstract = "Abstract",
                                   End = DateTime.Now.AddHours(1),
                                   LevelType = Level.General,
                                   RoomType = Room.CtrEFG,
                                   SpeakerId = _speakerId,
                                   Start = DateTime.Now
                               }
                       };
        }
    }
}
