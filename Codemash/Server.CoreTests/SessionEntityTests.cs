using Codemash.Api.Data;
using Codemash.Api.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Server.CoreTests
{
    [TestClass]
    public class SessionEntityTests
    {
        [TestMethod]
        public void test_that_when_session_is_instantiated_the_state_is_set_to_new()
        {
            // arrange
            Session session;

            // act
            session = new Session();

            // assert
            Assert.AreEqual(EntityState.New, session.CurrentState);
            Assert.AreEqual(true, session.IsDirty);
        }

        [TestMethod]
        public void test_that_given_a_new_session_instance_marking_as_unmodified_makes_entity_state_as_unmodified()
        {
            // arrange
            Session session = new Session();

            // act
            session.MarkUnmodified();

            // assert
            Assert.AreEqual(EntityState.Unmodified, session.CurrentState);
            Assert.AreEqual(false, session.IsDirty);
        }

        [TestMethod]
        public void test_that_given_a_new_session_instance_modifying_a_property_does_not_update_current_entity_state()
        {
            // arrange
            Session session = new Session();

            // act
            session.Title = "New Title";

            // assert
            Assert.AreEqual(EntityState.New, session.CurrentState);
        }

        [TestMethod]
        public void test_that_modifying_an_existing_entity_changes_the_state_to_modified()
        {
            // arrange
            Session session = new Session();

            // act
            session.MarkUnmodified();
            session.Title = "New Title";

            // assert
            Assert.AreEqual(EntityState.Modified, session.CurrentState);
        }

        [TestMethod]
        public void test_that_removing_a_new_entity_changes_the_current_state_to_removed()
        {
            // arrange
            Session session = new Session();

            // act
            session.MarkRemoved();

            // assert
            Assert.AreEqual(EntityState.Removed, session.CurrentState);
        }
    }
}
