using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.CoreTests.Factory;

namespace Server.CoreTests
{
    [TestClass]
    public class LoadedSessionChangeRepositoryTests
    {
        private ISessionChangeRepository _sessionChangeRepository;

        [TestInitialize]
        public void init()
        {
            _sessionChangeRepository = MoqSessionChangeRepositoryTestFactory.GetSessionChangeRepository();
            _sessionChangeRepository.Load();
        }

        [TestCleanup]
        public void end()
        {
            _sessionChangeRepository.Clear();
        }

        [TestMethod]
        public void test_add_range_to_empty_repository_adds_all_items()
        {
            var count = _sessionChangeRepository.GetAll().Count;
            _sessionChangeRepository.AddRange(new List<SessionChange>
                {
                    new SessionChange(),
                    new SessionChange(),
                    new SessionChange(),
                    new SessionChange()
                });

            Assert.AreEqual(4 + count, _sessionChangeRepository.GetAll().Count);
        }
    }
}
