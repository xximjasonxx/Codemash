using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Controllers;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Repositories;
using Codemash.DeltaApi.Controllers;
using Codemash.DeltaApi.Modules;
using DeltaApi.Web.Tests.Factory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;
using Ninject.Parameters;

namespace DeltaApi.Web.Tests
{
    [TestClass]
    public class SessionControllerTests
    {
        private IKernel _container;

        [TestInitialize]
        public void Initialize()
        {
            _container = new StandardKernel();
            _container.Load(new[] { new AssemblyIHttpControllerNinjectModule() });
        }

        [TestMethod]
        public void test_that_given_a_case_where_session_data_exists_in_the_database_get_should_return_a_non_empty_session_list()
        {
            // arrange
            var mockRepository = new Mock<ISessionRepository>();
            mockRepository.Setup(m => m.GetAll()).Returns(SessionDataFactory.GetStandardSessions());
            _container.Bind<ISessionRepository>().ToConstant(mockRepository.Object);
            var controller = (SessionController) _container.Get<IHttpController>("Session", new IParameter[0]);

            // act
            var result = controller.Get();

            // assert
            Assert.AreNotEqual(0, result.Count());
        }

        [TestMethod]
        public void test_that_given_a_case_where_there_are_no_sessions_in_the_session_repository_an_empty_list_should_be_returned()
        {
            // arrange
            var mockRepository = new Mock<ISessionRepository>();
            mockRepository.Setup(m => m.GetAll()).Returns(new List<Session>());
            _container.Bind<ISessionRepository>().ToConstant(mockRepository.Object);
            var controller = (SessionController) _container.Get<IHttpController>("Session", new IParameter[0]);

            // act
            var result = controller.Get();

            // assert
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void test_that_given_a_valid_session_id_that_session_is_returned_from_the_get_with_id_call()
        {
            // arrange
            var sessions = SessionDataFactory.GetStandardSessions();
            var mockRepository = new Mock<ISessionRepository>();
            mockRepository.Setup(m => m.Get(It.IsAny<int>())).Returns(sessions[0]);
            _container.Bind<ISessionRepository>().ToConstant(mockRepository.Object);

            // act
            var controller = (SessionController) _container.Get<IHttpController>("Session", new IParameter[0]);
            var result = controller.Get(1);

            // assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void test_that_if_an_invalid_id_is_given_an_http_response_exception_with_status_code_404_is_thrown()
        {
            // arrange
            var mockRepository = new Mock<ISessionRepository>();
            mockRepository.Setup(m => m.Get(It.IsAny<int>())).Returns(() => null);
            _container.Bind<ISessionRepository>().ToConstant(mockRepository.Object);

            // act
            var controller = (SessionController) _container.Get<IHttpController>("Session", new IParameter[0]);
            try
            {
                var result = controller.Get(int.MinValue);
            }
            catch(HttpResponseException responseException)
            {
                Assert.AreEqual(HttpStatusCode.NotFound, responseException.Response.StatusCode);
                throw;
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            _container.Dispose();
        }
    }
}
