using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codemash.Api.Data;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Ex;
using Codemash.Api.Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Server.CoreTests.Factory;

namespace Server.CoreTests
{
    [TestClass]
    public class LoadedSessionRepositoryTests
    {
        private ISessionRepository _sessionRepository;

        [TestInitialize]
        public void Init()
        {
            var container = new StandardKernel();
            container.Bind<ISessionRepository>().ToConstant(MoqSessionRepositoryFactory.GetSessionRepositoryMock());
            _sessionRepository = container.Get<ISessionRepository>();
            _sessionRepository.Load();
        }

        [TestMethod]
        public void test_get_with_valid_id_returns_non_null_result()
        {
            Assert.AreNotEqual(null, _sessionRepository.Get(193));
        }

        [TestMethod]
        public void test_get_with_invalid_id_value_returns_null_session_reference()
        {
            Assert.AreEqual(null, _sessionRepository.Get(int.MinValue));
        }

        [TestMethod]
        public void test_get_with_matching_predicate_returns_non_null_session_reference()
        {
            Assert.AreNotEqual(null, _sessionRepository.Get(s => s.Title.Length > 0));
        }

        [TestMethod]
        public void test_get_with_non_matching_predicate_returns_null_session_reference()
        {
            Assert.AreEqual(null, _sessionRepository.Get(s => s.Title == string.Empty));
        }

        [TestMethod]
        public void test_get_all_returns_non_empty_list()
        {
            Assert.AreNotEqual(0, _sessionRepository.GetAll());
        }

        [TestMethod]
        public void test_get_all_with_matching_predicate_return_non_empty_list()
        {
            Assert.AreNotEqual(0, _sessionRepository.GetAll(s => s.Title.StartsWith("P")).Count);
        }

        [TestMethod]
        public void test_get_all_with_non_matching_predicate_returns_non_empty_list()
        {
            Assert.AreEqual(0, _sessionRepository.GetAll(s => s.Title == string.Empty).Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ItemNotFoundException))]
        public void test_modifying_a_session_not_in_the_repository_throws_an_exception()
        {
            _sessionRepository.ModifySession(new SessionChange() { SessionId = int.MaxValue });
        }

        [TestMethod]
        public void test_modifying_a_session_in_the_repository_affects_the_session()
        {
            var affectedSessionId = _sessionRepository.GetAll()[0].SessionId;
            var change = new SessionChange()
                             {
                                 SessionId = affectedSessionId,
                                 Action = SessionChangeAction.Modify,
                                 Key = "Title",
                                 Value = "My New Title"
                             };

            // modify the session
            _sessionRepository.ModifySession(change);

            // get the session
            var session = _sessionRepository.Get(affectedSessionId);
            Assert.AreEqual("My New Title", session.Title);
        }
    }
}
