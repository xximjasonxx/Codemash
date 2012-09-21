using System.Web.Http.Dependencies;
using Codemash.DeltaApi.Controllers;
using Codemash.DeltaApi.Core;
using Codemash.DeltaApi.Dependency;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeltaApi.Web.Tests
{
    [TestClass]
    public class CodemashNinjectDependencyScopeTests
    {
        private IDependencyScope _dependencyScope;

        [TestInitialize]
        public void Initialize()
        {
            var container = new WebContainer(typeof (SessionsController).Assembly);
            _dependencyScope = new CodemashNinjectDependencyResolver(container);
        }

        [TestMethod]
        public void test_that_when_given_a_controller_type_that_the_controller_can_be_resolved_sucessfully()
        {
            var controller = _dependencyScope.GetService(typeof (SessionsController));
            Assert.IsNotNull(controller);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _dependencyScope.Dispose();
        }
    }
}
