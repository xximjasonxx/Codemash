using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunctionalTests
{
    [TestClass]
    public class ReadSessionChangeRepositoryIntegrationTests
    {
        private TransactionScope _transactionScope;

        [TestInitialize]
        public void Initialize()
        {
            _transactionScope = new TransactionScope();

            CreateTestData();
        }

        [TestCleanup]
        public void Cleanup()
        {
            
        }

        // private helper methods
        private void CreateTestData()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString))
            {
                connection.Open();
                
            }
        }
    }
}
