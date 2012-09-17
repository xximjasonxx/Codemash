using System.Linq;
using Codemash.Api.Data;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.CoreTests.Factory;

namespace Server.CoreTests
{
    [TestClass]
    public class SpeakerCompareTests
    {
        [TestMethod]
        public void test_that_when_given_two_identical_sets_of_speakers_no_differences_are_returned()
        {
            // arrange
            var masterSpeakers = MoqSpeakerDataFactory.GetSpeakersFromFile();
            var localSpeakers = MoqSpeakerDataFactory.GetSpeakersFromFile();

            // act
            var differences = masterSpeakers.Compare<Speaker, SpeakerChange>(localSpeakers);

            // assert
            Assert.AreEqual(0, differences.Count);
        }

        [TestMethod]
        public void test_that_when_given_two_sets_of_speakers_with_differences_among_existing_sessions_a_non_empty_list_of_differences_is_returned()
        {
            // arrange
            var masterSpeakers = MoqSpeakerDataFactory.GetSpeakersFromFile();
            var localSpeakers = MoqSpeakerDataFactory.GetSpeakersFromFile();

            // act
            masterSpeakers[0].FirstName = "Test";
            var differences = masterSpeakers.Compare<Speaker, SpeakerChange>(localSpeakers);

            // assert
            Assert.AreNotEqual(0, differences.Count);
        }

        [TestMethod]
        public void test_that_when_a_speaker_no_longer_appears_in_the_master_list_that_differences_are_recorded_of_type_remove()
        {
            // arrange
            var masterSpeakers = MoqSpeakerDataFactory.GetSpeakersFromFile();
            var localSpeakers = MoqSpeakerDataFactory.GetSpeakersFromFile();

            // act
            masterSpeakers.RemoveAt(0);
            var differences = masterSpeakers.Compare<Speaker, SpeakerChange>(localSpeakers);

            // assert
            Assert.AreNotEqual(0, differences.Count(d => d.ActionType == ChangeAction.Delete));
        }

        [TestMethod]
        public void test_that_when_a_speaker_appears_in_the_master_data_but_is_not_in_the_local_data_differences_are_recorded_of_type_add()
        {
            // arrange
            var masterSpeakers = MoqSpeakerDataFactory.GetSpeakersFromFile();
            var localSpeakers = MoqSpeakerDataFactory.GetSpeakersFromFile();

            // act
            localSpeakers.RemoveAt(0);
            var differences = masterSpeakers.Compare<Speaker, SpeakerChange>(localSpeakers);
            
            // assert
            Assert.AreNotEqual(0, differences.Count(d => d.ActionType == ChangeAction.Add));
        }
    }
}
