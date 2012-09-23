using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codemash.Api.Data;
using Codemash.Api.Data.Extensions;
using Codemash.Api.Data.Parsing.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Server.CoreTests
{
    [TestClass]
    public class EnumParsingTests
    {
        [TestMethod]
        public void test_that_parsing_a_valid_track_enum_string_with_no_spaces_returns_a_correct_enum()
        {
            var str = "Keynote";
            var enumParser = new TrackParse();
            Assert.AreEqual(Track.Keynote, enumParser.Parse(str, Track.Unknown));
        }

        [TestMethod]
        public void test_that_parsing_an_invalid_track_enum_string_returns_the_default_enum_value()
        {
            var str = "xxx";
            var enumParser = new TrackParse();
            Assert.AreEqual(Track.Unknown, enumParser.Parse(str, Track.Unknown));
        }

        [TestMethod]
        public void test_that_parsing_an_valid_track_name_with_spaces_returns_the_correct_enum()
        {
            var str = "Platforms and Tools";
            var enumParser = new TrackParse();
            Assert.AreEqual(Track.PlatformsTools, enumParser.Parse(str, Track.Unknown));
        }

        [TestMethod]
        public void test_that_parsing_a_valid_room_enum_string_with_no_spaces_returns_a_correct_enum()
        {
            var str = "Unknown";
            var enumParser = new RoomParse();
            Assert.AreEqual(Room.Unknown, enumParser.Parse(str, Room.Unknown));
        }

        [TestMethod]
        public void test_that_parsing_an_invalid_room_enum_string_returns_the_default_enum_value()
        {
            var str = "xxx";
            var enumParser = new RoomParse();
            Assert.AreEqual(Room.Unknown, enumParser.Parse(str, Room.Unknown));
        }

        [TestMethod]
        public void test_that_parsing_an_valid_room_name_with_spaces_returns_the_correct_enum()
        {
            var str = "Conv. Ctr. E, F, G";
            var enumParser = new RoomParse();
            Assert.AreEqual(Room.CtrEFG, enumParser.Parse(str, Room.Unknown));
        }

        [TestMethod]
        public void test_that_parsing_a_valid_level_string_returns_the_appropriate_enum()
        {
            var str = "Beginner";
            Assert.AreEqual(Level.Beginner, str.AsLevel(Level.Unknown));
        }

        [TestMethod]
        public void test_that_parsing_an_invalid_level_string_returns_the_default_enum_value()
        {
            var str = "abc";
            Assert.AreEqual(Level.Unknown, str.AsLevel(Level.Unknown));
        }

        [TestMethod]
        public void test_that_given_a_level_enum_reference_with_no_alternative_name_specified_the_tostring_is_returned()
        {
            // arrange
            var type = Level.Unknown;

            // act
            var value = type.AsDisplayString();

            // assert
            Assert.AreEqual(Level.Unknown.ToString(), value);
        }

        [TestMethod]
        public void test_that_given_a_room_reference_with_no_alternative_name_the_tostring_value_is_returned()
        {
            // arrange
            var type = Room.Unknown;

            // act
            var value = type.AsDisplayString();

            // assert
            Assert.AreEqual(Room.Unknown.ToString(), value);
        }

        [TestMethod]
        public void test_that_given_a_room_reference_with_an_alternative_name_returns_that_string_value()
        {
            // arrange
            var type = Room.CtrEFG;

            // act
            var value = type.AsDisplayString();

            // assert
            const string expected = "Conv. Ctr. E, F, G";
            Assert.AreEqual(expected, value);
        }

        [TestMethod]
        public void test_that_given_a_track_enum_reference_with_no_alternative_name_the_tostring_value_is_returned()
        {
            // arrange
            var type = Track.Unknown;

            // act
            var value = type.ToString();

            // assert
            Assert.AreEqual(Track.Unknown.ToString(), value);
        }

        [TestMethod]
        public void test_that_given_a_track_enum_reference_with_an_alternative_name_that_alternative_value_is_returned()
        {
            // arrange
            var type = Track.MobileDevelopment;

            // act
            var value = type.AsDisplayString();

            // assert
            const string expected = "Mobile Development";
            Assert.AreEqual(expected, value);
        }
    }
}
