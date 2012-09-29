using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using Codemash.Api.Data;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Repositories;
using Codemash.DeltaApi.Controllers;
using Codemash.DeltaApi.Modules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;
using Ninject.Modules;
using Ninject.Parameters;

namespace DeltaApi.Web.Tests
{
    [TestClass]
    public class SpeakerChangeControllerTests
    {
        private IKernel _container;

        [TestInitialize]
        public void Initalize()
        {
            _container = new StandardKernel(new INinjectModule[] { new AssemblyIHttpControllerNinjectModule() });
        }

        [TestMethod]
        public void test_that_given_a_repository_with_many_changesets_the_latest_changeset_is_returned_by_get()
        {
            // arrange
            var changes = new List<SpeakerChange>
                              {
                                  new SpeakerChange() { Version = 1 },
                                  new SpeakerChange() { Version = 1 },
                                  new SpeakerChange() { Version = 2 }
                              };
            var mock = new Mock<ISpeakerChangeRepository>();
            mock.Setup(m => m.GetLatest()).Returns(() =>
                                                    {
                                                        int latestVersion = changes.Max(c => c.Version);
                                                        return changes.Where(c => c.Version == latestVersion).ToList();
                                                    });

            _container.Bind<ISpeakerChangeRepository>().ToConstant(mock.Object);

            // act
            var controller = (SpeakerChangeController)_container.Get<IHttpController>("SpeakerChange", new IParameter[0]);
            var result = controller.Get();

            // assert
            Assert.AreNotEqual(0, result.Count());
        }

        [TestMethod]
        public void test_that_given_a_changeset_the_call_to_get_will_return_view_model_data_representing_the_change()
        {
            var mock = new Mock<ISpeakerChangeRepository>();
            mock.Setup(m => m.GetLatest()).Returns(new List<SpeakerChange>
                                                    {
                                                        new SpeakerChange { ActionType = ChangeAction.Add, SpeakerId = 123 }
                                                    });
            _container.Bind<ISpeakerChangeRepository>().ToConstant(mock.Object);

            // act
            var controller = (SpeakerChangeController)_container.Get<IHttpController>("SpeakerChange", new IParameter[0]);
            var result = controller.Get().First();

            // assert
            Assert.AreEqual(123, result.SpeakerId);
        }

        [TestMethod]
        public void test_that_specific_changesets_can_be_extracted_by_their_version_number()
        {
            // arrange
            var changes = new List<SpeakerChange>
                              {
                                  new SpeakerChange() { Version = 1},
                                  new SpeakerChange() { Version = 1},
                                  new SpeakerChange() { Version = 2},
                              };

            var mock = new Mock<ISpeakerChangeRepository>();
            mock.Setup(m => m.GetAll(It.IsAny<int>()))
                .Returns((int version) => changes.Where(sc => sc.Version == version).ToList());
            _container.Bind<ISpeakerChangeRepository>().ToConstant(mock.Object);

            // act
            var controller = (SpeakerChangeController)_container.Get<IHttpController>("SpeakerChange", new IParameter[0]);
            var result = controller.Get(2);

            // assert
            Assert.AreEqual(1, result.Count());
        }

        [TestCleanup]
        public void Cleanup()
        {
            _container.Dispose();
        }
    }
}
