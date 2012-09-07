using Codemash.Api.Data.Ex;
using Codemash.Api.Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Server.CoreTests.Factory;

namespace Server.CoreTests
{
    [TestClass]
    public class UnloadedSpeakerRepositoryTests
    {
        private ISpeakerRepository _speakerRepository;

        [TestInitialize]
        public void Init()
        {
            var kernel = new StandardKernel();
            kernel.Bind<ISpeakerRepository>().ToConstant(MoqSpeakerRepositoryFactory.GetSpeakerRepositoryMock());
            _speakerRepository = kernel.Get<ISpeakerRepository>();
            _speakerRepository.Clear();
        }

        [TestMethod]
        [ExpectedException(typeof(RepositoryNotLoadedException))]
        public void test_call_to_get_for_key_throws_exception_for_unloaded_repository()
        {
            _speakerRepository.Get(int.MaxValue);
        }

        [TestMethod]
        [ExpectedException(typeof(RepositoryNotLoadedException))]
        public void test_call_to_get_for_predicate_throws_exception_for_unloaded_repository()
        {
            _speakerRepository.Get(s => true);
        }

        [TestMethod]
        [ExpectedException(typeof(RepositoryNotLoadedException))]
        public void test_call_to_general_get_all_throws_exception_for_unloaded_repository()
        {
            _speakerRepository.GetAll();
        }

        [TestMethod]
        [ExpectedException(typeof(RepositoryNotLoadedException))]
        public void test_call_to_general_get_for_preidcate_throws_exception_for_unloaded_repository()
        {
            _speakerRepository.GetAll(s => true);
        }
    }
}
