using System.Linq;
using Codemash.Api.Data.Repositories.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunctionalTests
{
    [TestClass]
    public class ModelInitialization
    {
        [TestMethod]
        [Ignore]
        public void InitializeDatabase()
        {
            using (var context = new CodemashContext())
            {
                var list = context.SessionChanges.ToList();
            }
        }
    }
}
