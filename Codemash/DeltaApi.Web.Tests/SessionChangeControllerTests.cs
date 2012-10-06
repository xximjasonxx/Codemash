using System.Collections.Generic;
using System.Linq;
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
    public class SessionChangeControllerTests
    {
        private IKernel _container;

        [TestInitialize]
        public void Initialize()
        {
            _container = new StandardKernel(new INinjectModule[] { new AssemblyIHttpControllerNinjectModule() });
        }

        [TestMethod]
        public void test_that_get_returns_only_the_latest_changeset()
        {
            // arrange
            var changes = new List<SessionChange>
                               {
                                   new SessionChange {ActionType = ChangeAction.Add, SessionId = 123, Version = 1},
                                   new SessionChange
                                       {
                                           ActionType = ChangeAction.Modify,
                                           SessionId = 124,
                                           Key = "Title",
                                           Value = "new Title",
                                           Version = 1
                                       },
                                   new SessionChange {ActionType = ChangeAction.Delete, SessionId = 125, Version = 2},
                               };

            var mock = new Mock<ISessionChangeRepository>();
            mock.Setup(m => m.GetLatest()).Returns(() =>
                                                       {
                                                           int latestVersion = changes.Max(s => s.Version);
                                                           return changes.Where(sc => sc.Version == latestVersion).ToList();
                                                       });
            _container.Bind<ISessionChangeRepository>().ToConstant(mock.Object);

            // act
            var controller = (SessionChangeController)_container.Get<IHttpController>("SessionChange", new IParameter[0]);
            var result = controller.Get();

            // assert
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void test_that_when_multiple_versions_of_changesets_exist_in_the_data_store_calling_get_all_with_the_version_returns_all_changeset_information_for_that_version()
        {
            // arrange
            var mock = new Mock<ISessionChangeRepository>();
            var sessionChanges = new List<SessionChange>
                                     {
                                         new SessionChange {Version = 1},
                                         new SessionChange {Version = 1},
                                         new SessionChange {Version = 2}
                                     };
            mock.Setup(m => m.GetAll(It.IsAny<int>())).Returns((int version) => sessionChanges.Where(sc => sc.Version == version).ToList());
            _container.Bind<ISessionChangeRepository>().ToConstant(mock.Object);

            // act
            var controller = (SessionChangeController)_container.Get<IHttpController>("SessionChange", new IParameter[0]);
            var result = controller.Get(1);

            // assert
            Assert.AreEqual(2, result.Count());
        }

        [TestCleanup]
        public void Cleanup()
        {
            _container.Dispose();
        }
    }
}
