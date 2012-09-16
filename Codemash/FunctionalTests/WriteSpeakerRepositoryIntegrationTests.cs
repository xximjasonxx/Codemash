using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Repositories;
using Codemash.Poller.Container;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace FunctionalTests
{
    [TestClass]
    public class WriteSpeakerRepositoryIntegrationTests
    {
        private TransactionScope _transactionScope;

        [TestInitialize]
        public void Initialize()
        {
            _transactionScope = new TransactionScope();
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_given_a_range_of_speakers_ensure_that_all_items_are_written_to_the_database()
        {
            // arrange
            var repository = new PollerContainer().Get<ISpeakerRepository>();

            // assert pt. 1
            Assert.AreEqual(0, repository.GetAll().Count);

            // act
            var speakers = GetSpeakerData().ToList();
            repository.SaveRange(speakers);

            // assert pt. 2
            Assert.AreEqual(speakers.Count, repository.GetAll().Count);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_given_a_range_of_speakers_which_match_existing_speakers_changes_to_speakers_are_persisted_in_the_databsae()
        {
            // arrange
            var repository = new PollerContainer().Get<ISpeakerRepository>();
            var speakers = GetSpeakerData();
            var orginalSpeakerCount = speakers.Count;

            // add
            repository.SaveRange(speakers);

            // act
            speakers = repository.GetAll();
            speakers[0].FirstName = "Kerry";
            repository.SaveRange(speakers);

            // assert
            speakers = repository.GetAll();
            Assert.AreEqual(orginalSpeakerCount, speakers.Count);
            Assert.AreEqual("Kerry", speakers[0].FirstName);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void test_that_if_a_speaker_exists_in_the_given_range_but_not_in_the_repository_the_speaker_is_added()
        {
            // arrange
            var repository = new PollerContainer().Get<ISpeakerRepository>();
            var speakers = GetSpeakerData();

            // add
            repository.SaveRange(speakers.Take(2));

            // act
            speakers = repository.GetAll();
            var speaker = GetSpeakerData().First();
            speaker.SpeakerId = 4;
            speakers.Add(speaker);
            repository.SaveRange(speakers);
            speakers = repository.GetAll();

            // assert
            Assert.AreEqual(3, speakers.Count);

        }

        [TestMethod]
        public void test_that_if_a_speaker_exists_in_the_repository_but_not_in_the_speaker_range_it_is_removed_from_the_repository()
        {
            // arrange
            var repository = new PollerContainer().Get<ISpeakerRepository>();
            
            // add
            repository.SaveRange(GetSpeakerData());

            // act
            var list = repository.GetAll().Take(2).ToList();
            var listCount = list.Count;
            repository.SaveRange(list);

            // assert
            Assert.AreEqual(listCount, repository.GetAll().Count);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _transactionScope.Dispose();
        }

        // private methods
        private IList<Speaker> GetSpeakerData()
        {
            return new List<Speaker>
                       {
                           new Speaker
                               {
                                   SpeakerId = 1,
                                   Biography = "Biography 1",
                                   EmailAddress = "test@example.com",
                                   FirstName = "Sam",
                                   LastName = "Jones"
                               },
                           new Speaker
                               {
                                   SpeakerId = 2,
                                   Biography = "Biography 2",
                                   EmailAddress = "test@example.com",
                                   FirstName = "Sam",
                                   LastName = "Jones"
                               },
                           new Speaker
                               {
                                   SpeakerId = 3,
                                   Biography = "Biography 3",
                                   EmailAddress = "test@example.com",
                                   FirstName = "Sam",
                                   LastName = "Jones"
                               }
                       };
        }
    }
}
