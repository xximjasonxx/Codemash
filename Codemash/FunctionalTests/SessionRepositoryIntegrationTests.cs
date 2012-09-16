using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;
using Codemash.Api.Data.Repositories.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunctionalTests
{
    [TestClass]
    public class SessionRepositoryIntegrationTests
    {
        private TransactionScope _transactionScope;

        [TestInitialize]
        public void TestInitialize()
        {
            // start the transaction
            _transactionScope = new TransactionScope();

        }

        [TestMethod]
        public void test_that_session_data_can_retrieved_using_get_all()
        {
            using (var context = new CodemashContext())
            {
                
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _transactionScope.Dispose();
        }
    }
}
