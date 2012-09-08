using Codemash.Api.Data.Ex;
using Codemash.Api.Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Server.CoreTests.Factory;

namespace Server.CoreTests
{
    [TestClass]
    public class LoadedSpeakerRespositoryTests
    {
        private ISpeakerRepository _speakerRepository;

        [TestInitialize]
        public void Init()
        {
            var kernel = new StandardKernel();
            kernel.Bind<ISpeakerRepository>().ToConstant(MoqSpeakerRepositoryFactory.GetSpeakerRepositoryMock());
            _speakerRepository = kernel.Get<ISpeakerRepository>();
            _speakerRepository.Load();
        }

        [TestMethod]
        [ExpectedException(typeof(RepositoryNotLoadedException))]
        public void test_clear_resets_repository_state()
        {
            _speakerRepository.Clear();
            _speakerRepository.Get(1);
        }

        [TestMethod]
        public void test_that_given_a_valid_id_the_repository_will_return_a_non_null_instance_of_speaker()
        {
            var speaker = _speakerRepository.Get(113);
            Assert.AreNotEqual(null, speaker);
        }

        [TestMethod]
        public void test_that_given_an_invalid_id_the_repository_will_return_a_null_instance_of_speaker()
        {
            var speaker = _speakerRepository.Get(int.MinValue);
            Assert.AreEqual(null, speaker);
        }

        [TestMethod]
        public void test_that_given_a_valid_predicate_repository_returns_an_instance_of_speaker()
        {
            var speaker = _speakerRepository.Get(s => s.FirstName.Length > 0);
            Assert.AreNotEqual(null, speaker);
        }

        [TestMethod]
        public void test_that_given_an_invalid_predicate_matching_no_speaker_returns_a_null_speaker_instance()
        {
            var speaker = _speakerRepository.Get(s => s.FirstName == string.Empty);
            Assert.AreEqual(null, speaker);
        }

        [TestMethod]
        public void test_that_a_call_to_get_all_returns_a_non_null_non_empty_list()
        {
            var list = _speakerRepository.GetAll();
            Assert.AreNotEqual(null, list);
            Assert.AreNotEqual(0, list.Count);
        }

        [TestMethod]
        public void test_a_call_to_getall_with_a_matchable_predicate_returns_a_non_null_non_empty_list()
        {
            var list = _speakerRepository.GetAll(s => s.SpeakerId > 100);
            Assert.AreNotEqual(null, list);
            Assert.AreNotEqual(0, list.Count);
        }

        [TestMethod]
        public void test_calling_getall_with_non_matching_predicate_returns_non_null_empty_list()
        {
            var list = _speakerRepository.GetAll(s => s.SpeakerId == 0);
            Assert.AreNotEqual(null, list);
            Assert.AreEqual(0, list.Count);
        }
    }
}
