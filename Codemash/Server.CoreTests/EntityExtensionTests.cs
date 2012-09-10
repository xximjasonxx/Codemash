using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Ex;
using Codemash.Api.Data.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Server.CoreTests
{
    [TestClass]
    public class EntityExtensionTests
    {
        [TestMethod]
        [ExpectedException(typeof(PropertyNotFoundException))]
        public void test_that_if_an_attempt_to_update_a_non_existing_property_is_made_an_exception_is_raised()
        {
            var session = new Session();
            session.ApplyChange("MyProperty", "this new value");
        }

        [TestMethod]
        public void test_that_a_property_is_updated_with_the_correct_value_when_apply_change_is_called()
        {
            var session = new Session();
            session.ApplyChange("Title", "MyTest");
            Assert.AreEqual(session.Title, "MyTest");
        }
    }
}
