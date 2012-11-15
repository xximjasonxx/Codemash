using System.Linq;
using Codemash.Api.Data;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Server.CoreTests.Containers;
using Server.CoreTests.Factory;

namespace Server.CoreTests
{
    [TestClass]
    public class SessionListComparisonTests
    {
        private TestDataFactory _dataFactory;

        [TestInitialize]
        public void Init()
        {
            _dataFactory = new TestContainer().Get<TestDataFactory>();
        }

        [TestMethod]
        public void test_that_comparing_two_equivalent_session_lists_empty_list_is_returned()
        {
            var masterSessionList = _dataFactory.GetSessions();
            var localSessionList = _dataFactory.GetSessions();

            var changesList = masterSessionList.Compare(localSessionList);
            Assert.AreNotEqual(null, changesList);
            Assert.AreEqual(0, changesList.Count);
        }

        [TestMethod]
        public void test_that_comparing_two_session_lists_where_one_difference_exists_returns_non_empty_list()
        {
            var masterList = _dataFactory.GetSessions();
            var childList = _dataFactory.GetSessions();
            var session = childList.First();

            // modify the title
            session.Title = "This is a Test";

            var changesList = masterList.Compare(childList);
            Assert.AreNotEqual(0, changesList.Count);
        }

        [TestMethod]
        public void test_that_differences_across_objects_within_a_set_are_picked_up_by_comparison()
        {
            var masterList = _dataFactory.GetSessions();
            var childList = _dataFactory.GetSessions();

            childList[0].Title = "I have been changed";
            childList[0].Room = "Indigo Bay";
            childList[1].Abstract = "I am a new abstract";
            childList[1].Start = childList[1].Start.AddHours(1);

            var differences = masterList.Compare(childList);
            Assert.AreEqual(4, differences.Count);
        }

        [TestMethod]
        public void test_that_a_session_removed_from_the_master_is_recorded_as_a_delete_action_for_session_change()
        {
            var masterList = _dataFactory.GetSessions();
            var localList = _dataFactory.GetSessions();

            // remove the first session
            masterList.RemoveAt(0);

            var differences = masterList.Compare(localList);
            Assert.AreNotEqual(0, differences.Count);
        }

        [TestMethod]
        public void test_that_a_session_added_to_master_is_recorded_as_an_add_action_for_all_properties()
        {
            var masterList = _dataFactory.GetSessions();
            var localList = _dataFactory.GetSessions();

            // remove the first session from local
            localList.RemoveAt(0);

            var differences = masterList.Compare(localList);
            Assert.AreNotEqual(0, differences.Count);
        }
    }
}
