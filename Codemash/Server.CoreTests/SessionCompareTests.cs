using System.Linq;
using Codemash.Api.Data;
using Codemash.Api.Data.Compare;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.CoreTests.Factory;

namespace Server.CoreTests
{
    [TestClass]
    public class SessionCompareTests
    {
        [TestMethod]
        public void test_that_comparing_two_equivalent_session_lists_empty_list_is_returned()
        {
            var masterSessionList = MoqSessionCompareTestFactory.GetStandardSessionList();
            var localSessionList = MoqSessionCompareTestFactory.GetStandardSessionList();

            var changesList = new SessionCompare().CompareSessionLists(masterSessionList, localSessionList);
            Assert.AreNotEqual(null, changesList);
            Assert.AreEqual(0, changesList.Count);
        }

        [TestMethod]
        public void test_that_comparing_two_session_lists_where_one_difference_exists_returns_non_empty_list()
        {
            var masterList = MoqSessionCompareTestFactory.GetStandardSessionList();
            var childList = MoqSessionCompareTestFactory.GetStandardSessionList();
            var session = childList.First();

            // modify the title
            session.Title = "This is a Test";

            var changesList = new SessionCompare().CompareSessionLists(masterList, childList);
            Assert.AreNotEqual(0, changesList.Count);
        }

        [TestMethod]
        public void test_that_differences_across_objects_within_a_set_are_picked_up_by_comparison()
        {
            var masterList = MoqSessionCompareTestFactory.GetStandardSessionList();
            var childList = MoqSessionCompareTestFactory.GetStandardSessionList();

            childList[0].Title = "I have been changed";
            childList[0].Room = Room.Ctr14;
            childList[1].Abstract = "I am a new abstract";
            childList[1].Start = childList[1].Start.AddHours(1);

            var differences = new SessionCompare().CompareSessionLists(masterList, childList);
            Assert.AreEqual(4, differences.Count);
        }

        [TestMethod]
        public void test_that_a_session_removed_from_the_master_is_recorded_as_a_delete_action_for_session_change()
        {
            var masterList = MoqSessionCompareTestFactory.GetStandardSessionList();
            var localList = MoqSessionCompareTestFactory.GetStandardSessionList();

            // remove the first session
            masterList.RemoveAt(0);

            var differences = new SessionCompare().CompareSessionLists(masterList, localList);
            Assert.AreNotEqual(0, differences.Count);
        }

        [TestMethod]
        public void test_that_a_session_added_to_master_is_recorded_as_an_add_action_for_all_properties()
        {
            var masterList = MoqSessionCompareTestFactory.GetStandardSessionList();
            var localList = MoqSessionCompareTestFactory.GetStandardSessionList();

            // remove the first session from local
            localList.RemoveAt(0);

            var differences = new SessionCompare().CompareSessionLists(masterList, localList);
            Assert.AreNotEqual(0, differences.Count);
        }
    }
}
