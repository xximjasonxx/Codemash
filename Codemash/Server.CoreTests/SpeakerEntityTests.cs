using Codemash.Api.Data;
using Codemash.Api.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Server.CoreTests
{
    [TestClass]
    public class SpeakerEntityTests
    {
        [TestMethod]
        public void test_that_when_a_speaker_instance_is_created_the_entity_state_is_new()
        {
            // arrange
            var speaker = new Speaker();

            // assert
            Assert.AreEqual(EntityState.New, speaker.CurrentState);
        }

        [TestMethod]
        public void test_that_if_a_modification_to_a_new_speaker_instance_is_made_entity_state_remains_new()
        {
            // arrange
            var speaker = new Speaker();

            // act
            speaker.FirstName = "New First Name";

            // assert
            Assert.AreEqual(EntityState.New, speaker.CurrentState);
        }

        [TestMethod]
        public void test_that_when_mark_unmodified_is_called_on_speaker_instance_status_becomes_unmodified()
        {
            // arrange
            var speaker = new Speaker();

            // act
            speaker.MarkUnmodified();

            // assert
            Assert.AreEqual(EntityState.Unmodified, speaker.CurrentState);
        }

        [TestMethod]
        public void test_that_if_a_property_is_modified_on_a_status_unmodified_new_status_becomes_modified()
        {
            // arrange
            var speaker = new Speaker();

            // act
            speaker.MarkUnmodified();
            speaker.FirstName = "New First Name";

            // assert
            Assert.AreEqual(EntityState.Modified, speaker.CurrentState);
        }

        [TestMethod]
        public void test_that_if_a_new_instance_of_speaker_is_marked_removed_status_changes_to_deleted()
        {
            // arrange
            var speaker = new Speaker();

            // act
            speaker.MarkRemoved();

            // assert
            Assert.AreEqual(EntityState.Removed, speaker.CurrentState);
        }
    }
}
