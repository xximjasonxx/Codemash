using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codemash.Api.Data.Ex;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.CoreTests.Factory;

namespace Server.CoreTests
{
    [TestClass]
    public class UnloadedSessionChangeRepositoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(RepositoryNotLoadedException))]
        public void test_get_all_throws_exception_when_repository_is_not_loaded()
        {
            var repository = MoqSessionChangeRepositoryTestFactory.GetSessionChangeRepository();
            repository.GetAll();
        }
    }
}
