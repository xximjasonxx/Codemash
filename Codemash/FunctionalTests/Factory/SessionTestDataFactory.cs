using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace FunctionalTests.Factory
{
    public static class SessionTestDataFactory
    {
        public static void AddStandardTestData()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString))
            {
                connection.Open();
            }
        }
    }
}
