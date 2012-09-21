using Codemash.DeltaApi.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeltaApi.Web.Tests
{
    [TestClass]
    public class ParsingExtensionsTest
    {
        [TestMethod]
        public void test_that_when_given_a_type_with_the_controller_name_format_the_proper_controller_name_is_extracted()
        {
            const string controllerTypeName = "HomeController";
            var controllerName = controllerTypeName.AsControllerName();

            Assert.AreEqual("Home", controllerName);
        }

        [TestMethod]
        public void test_that_when_given_any_type_name_the_appropriate_controller_name_is_extracted()
        {
            const string controllerTypeName = "PersonController";
            var controllerName = controllerTypeName.AsControllerName();

            Assert.AreEqual("Person", controllerName);
        }

        [TestMethod]
        public void test_that_given_a_type_name_not_in_controller_format_an_empty_string_should_be_returned()
        {
            const string typeName = "SomeType";
            var controllerName = typeName.AsControllerName();

            Assert.AreEqual(string.Empty, controllerName);
        }
    }
}
