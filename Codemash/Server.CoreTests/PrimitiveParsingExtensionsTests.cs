using System;
using Codemash.Server.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Server.CoreTests
{
    [TestClass]
    public class PrimitiveParsingExtensionsTests
    {
        [TestMethod]
        public void test_that_given_a_string_representing_a_valid_integer_method_parses_to_int_value()
        {
            var str = "5";
            Assert.AreEqual(5, str.AsInt());
        }

        [TestMethod]
        public void test_that_given_a_string_not_representing_a_valid_integer_method_returns_minimum_int_value()
        {
            var str = "abc";
            Assert.AreEqual(int.MinValue, str.AsInt());
        }

        [TestMethod]
        public void test_that_given_a_valid_date_string_method_will_return_a_DateTime_representing_the_given_string()
        {
            var str = "1/13/1983";
            var dt = str.AsDateTime();
            Assert.AreNotEqual(DateTime.MinValue, dt);

            Assert.AreEqual(1, dt.Month);
            Assert.AreEqual(13, dt.Day);
            Assert.AreEqual(1983, dt.Year);
        }

        [TestMethod]
        public void test_that_given_an_invalid_date_string_datetime_min_value_is_returned()
        {
            var str = "abc";
            Assert.AreEqual(DateTime.MinValue, str.AsDateTime());
        }
    }
}
