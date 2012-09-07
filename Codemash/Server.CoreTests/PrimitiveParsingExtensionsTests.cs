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
    }
}
