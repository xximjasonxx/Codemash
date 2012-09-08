using System.Collections.Generic;
using System.Linq;
using Codemash.Api.Data.Entities;
using Codemash.Server.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Server.CoreTests
{
    [TestClass]
    public class ListManipulationsTests
    {
        [TestMethod]
        public void test_that_action_is_universally_applied_to_all_items()
        {
            var list = new List<Session>
                {
                    new Session() { Title = "abc" },
                    new Session() { Title = "def" },
                    new Session() { Title = "ghi" }
                };

            list.Apply(li => li.Title = li.Title.Substring(0,1));
            Assert.AreEqual(0, list.Count(s => s.Title.Length > 1));
        }
    }
}
