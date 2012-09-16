using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codemash.Api.Data;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Extensions;
using Codemash.Api.Data.Parsing.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.CoreTests.Factory;

namespace Server.CoreTests
{
    [TestClass]
    public class CompareExtensionMethodTests
    {
        [TestMethod]
        public void test_when_comparing_two_sessions_whose_values_are_identical_an_empty_dictionary_is_returned()
        {
            var session = MoqSessionCompareTestFactory.GetStandardSessionList().First();
            var result = session.CompareTo(session);

            Assert.AreNotEqual(null, result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void test_when_comparing_sessions_which_are_different_a_non_empty_dictionary_is_returned()
        {
            var session1 = MoqSessionCompareTestFactory.GetStandardSessionList().First();
            var session2 = MoqSessionCompareTestFactory.GetStandardSessionList().First();
            session2.Title = "Programming Style";
            var result = session2.CompareTo(session1);

            Assert.AreNotEqual(null, result);
            Assert.AreNotEqual(0, result.Count);
        }

        [TestMethod]
        public void test_when_comparing_session_with_different_start_times_the_value_is_properly_translated()
        {
            var session1 = MoqSessionCompareTestFactory.GetStandardSessionList().First();
            var session2 = MoqSessionCompareTestFactory.GetStandardSessionList().First();
            session2.Start = session2.Start.AddHours(1);

            var result = session1.CompareTo(session2);
            Assert.AreEqual(true, result.ContainsKey("Start"));
            Assert.AreEqual(session1.Start.ToString(), result["Start"]);
        }

        [TestMethod]
        public void test_when_comparing_sessions_with_track_differences_the_value_returned_is_correct()
        {
            var session1 = MoqSessionCompareTestFactory.GetStandardSessionList().First();
            var session2 = MoqSessionCompareTestFactory.GetStandardSessionList().First();
            session2.TrackType = Track.ProfessionalDevelopment;

            var trackParser = new TrackParse();
            var result = session1.CompareTo(session2);
            Assert.AreEqual(true, result.ContainsKey("Track"));
            Assert.AreEqual(session1.TrackType, trackParser.Parse(result["Track"], Track.Unknown));
        }

        [TestMethod]
        public void test_when_comparing_sessions_with_multiple_property_differences_dictionary_has_equivalent_count()
        {
            // test for two differences
            var session1 = MoqSessionCompareTestFactory.GetStandardSessionList().First();
            var session2 = MoqSessionCompareTestFactory.GetStandardSessionList().First();

            session2.Title = "This is a Test";
            session2.Start = session1.Start.AddDays(-1);

            var result = session1.CompareTo(session2);
            Assert.AreEqual(2, result.Count);
        }
    }
}
