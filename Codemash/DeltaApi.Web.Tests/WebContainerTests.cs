using System.Web.Http.Controllers;
using Codemash.Api.Data.Repositories;
using Codemash.DeltaApi.Controllers;
using Codemash.DeltaApi.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ninject.Parameters;

namespace DeltaApi.Web.Tests
{
    [TestClass]
    public class WebContainerTests
    {
        [TestMethod]
        public void test_that_the_web_container_can_be_instantiated()
        {
            var container = new WebContainer();
            Assert.IsNotNull(container);
        }

        [TestMethod]
        public void test_that_a_controller_can_be_extracted_by_interface_and_meta_name()
        {
            var container = new WebContainer();
            var controller = container.TryGet<IHttpController>("Session", new IParameter[0]);

            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void test_that_session_repository_can_be_fulfilled_by_the_container()
        {
            var container = new WebContainer();
            var repository = container.TryGet<ISessionRepository>();

            Assert.IsNotNull(repository);
        }
    }
}
