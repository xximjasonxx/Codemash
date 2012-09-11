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
        }

        [TestCleanup]
        public void end()
        {
            _sessionChangeRepository.Clear();
        }
    }
}
