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
using Ninject.Modules;
using Ninject.Parameters;

namespace DeltaApi.Web.Tests
{
    [TestClass]
    public class SpeakerControllerTests
    {
        private IKernel _container;

        [TestInitialize]
        public void Initialize()
        {
            _container = new StandardKernel(new INinjectModule[] { new AssemblyIHttpControllerNinjectModule() });
        }

        [TestMethod]
        public void test_that_call_get_returns_a_non_empty_list_when_data_exists_in_the_repository()
        {
            // arrange
            var mock = new Mock<ISpeakerRepository>();
            mock.Setup(m => m.GetAll()).Returns(SpeakerDataFactory.GetSpeakersFromFile());
            _container.Bind<ISpeakerRepository>().ToConstant(mock.Object);

            // act
            var controller = (SpeakerController) _container.Get<IHttpController>("Speaker", new IParameter[0]);
            var result = controller.Get();

            // assert
            Assert.AreNotEqual(0, result.Count());
        }

        [TestMethod]
        public void test_that_given_an_empty_repository_get_returns_an_empty_list_when_called()
        {
            // arrange
            var mock = new Mock<ISpeakerRepository>();
            mock.Setup(m => m.GetAll()).Returns(new List<Speaker>());
            _container.Bind<ISpeakerRepository>().ToConstant(mock.Object);

            // act
            var controller = (SpeakerController) _container.Get<IHttpController>("Speaker", new IParameter[0]);
            var result = controller.Get();

            // assert
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void test_that_given_a_valid_identifier_get_returns_a_non_speaker_references()
        {
            // arrange
            var speakers = SpeakerDataFactory.GetSpeakersFromFile();
            var mock = new Mock<ISpeakerRepository>();
            mock.Setup(m => m.Get(It.IsAny<int>())).Returns(speakers[0]);
            _container.Bind<ISpeakerRepository>().ToConstant(mock.Object);

            // act
            var controller = (SpeakerController) _container.Get<IHttpController>("Speaker", new IParameter[0]);
            var result = controller.Get(1);

            // assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void that_that_given_an_invalid_identifier_an_exception_is_thrown_representing_status_code_404()
        {
            // arrange
            var mock = new Mock<ISpeakerRepository>();
            mock.Setup(m => m.Get(It.IsAny<int>())).Returns(() => null);
            _container.Bind<ISpeakerRepository>().ToConstant(mock.Object);

            // act
            var controller = (SpeakerController) _container.Get<IHttpController>("Speaker", new IParameter[0]);
            try
            {
                controller.Get(1);
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
