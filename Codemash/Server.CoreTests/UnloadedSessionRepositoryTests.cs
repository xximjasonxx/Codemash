using Codemash.Api.Data.Ex;
using Codemash.Api.Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Server.CoreTests.Factory;

namespace Server.CoreTests
{
    [TestClass]
    public class UnloadedSessionRepositoryTests
    {
        private ISessionRepository _sessionRepository;

        [TestInitialize]
        public void Init()
        {
            var container = new StandardKernel();
            container.Bind<ISessionRepository>().ToConstant(MoqSessionRepositoryFactory.GetSessionRepositoryMock());
            _sessionRepository = container.Get<ISessionRepository>();
            _sessionRepository.Clear();
        }

        [TestMethod]
        [ExpectedException(typeof(RepositoryNotLoadedException))]
        public void test_that_calling_get_all_before_load_throws_an_exception()
        {
            _sessionRepository.GetAll();
        }

        [TestMethod]
        [ExpectedException(typeof(RepositoryNotLoadedException))]
        public void test_that_calling_get_all_with_predicate_before_load_throws_an_exception()
        {
            _sessionRepository.GetAll(s => true);
        }

        [TestMethod]
        [ExpectedException(typeof(RepositoryNotLoadedException))]
        public void test_that_calling_get_before_load_throws_an_exception()
        {
            _sessionRepository.Get(1);
        }

        [TestMethod]
        [ExpectedException(typeof(RepositoryNotLoadedException))]
        public void test_that_calling_get_with_predicate_before_load_throws_an_exception()
        {
            _sessionRepository.Get(s => true);
        }
    }
}
