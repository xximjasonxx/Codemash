using System.Transactions;
using Codemash.Api.Data.Repositories;
using Codemash.Poller.Container;
using FunctionalTests.Factory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace FunctionalTests
{
    [TestClass]
    public class ReadSpeakerRepositoryIntegrationTests
    {
        private TransactionScope _transactionScope;

        [TestInitialize]
        public void Initialize()
        {
            _transactionScope = new TransactionScope();

            // create test data
            SpeakerTestDataFactory.CreateStandardSpeakers();
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_getall_returns_all_speakers_from_database_table()
        {
            // arrange
            var repository = new PollerContainer().Get<ISpeakerRepository>();

            // act
            var list = repository.GetAll();

            // assert
            Assert.AreNotEqual(0, list.Count);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_getall_with_matching_condition_returns_non_empty_list_from_database()
        {
            // arrange
            var repository = new PollerContainer().Get<ISpeakerRepository>();

            // act
            var list = repository.GetAll(sp => sp.Name != string.Empty);

            // assert
            Assert.AreNotEqual(0, list.Count);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_get_by_a_valid_key_value_returns_a_non_null_speaker_with_the_given_key()
        {
            // arrange
            var repository = new PollerContainer().Get<ISpeakerRepository>();

            // act
            var speakerId = SpeakerTestDataFactory.GetValidSpeakerId();
            var speaker = repository.Get(speakerId);

            // assert
            Assert.IsNotNull(speaker);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_get_with_an_invalid_key_value_returns_a_null_speaker_reference()
        {
            // arrange
            var repository = new PollerContainer().Get<ISpeakerRepository>();

            // act
            var speaker = repository.Get(int.MaxValue);

            // assert
            Assert.IsNull(speaker);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_get_with_a_condition_matching_a_speaker_returns_a_non_null_instance()
        {
            // arrange
            var repository = new PollerContainer().Get<ISpeakerRepository>();

            // act
            var speaker = repository.Get(sp => sp.Name != string.Empty);

            // assert
            Assert.IsNotNull(speaker);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_a_get_with_a_condition_matching_no_items_returns_a_null_instance()
        {
            // arrange
            var repository = new PollerContainer().Get<ISpeakerRepository>();

            // act
            var speaker = repository.Get(sp => sp.Name == null);

            // assert
            Assert.IsNull(speaker);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _transactionScope.Dispose();
        }
    }
}
