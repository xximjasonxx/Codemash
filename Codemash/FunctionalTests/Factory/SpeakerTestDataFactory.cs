using System.Configuration;
using System.Data.SqlClient;
using Codemash.Server.Core.Extensions;

namespace FunctionalTests.Factory
{
    public static class SpeakerTestDataFactory
    {
        public static void CreateStandardSpeakers()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString))
            {
                connection.Open();
                const string cmdText = "insert into Speakers(Biography, Twitter, EmailAddress, BlogUrl, FirstName, LastName, Company) values(@Bio, @Twitter, @Email, @Blog, @FirstName, @LastName, @Company)";
                using (var command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("@Bio", "My Biography");
                    command.Parameters.AddWithValue("@Twitter", string.Empty);
                    command.Parameters.AddWithValue("@Email", "test@example.com");
                    command.Parameters.AddWithValue("@Blog", string.Empty);
                    command.Parameters.AddWithValue("@FirstName", "Troy");
                    command.Parameters.AddWithValue("@LastName", "Smith");
                    command.Parameters.AddWithValue("@Company", string.Empty);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@Bio", "My Biography Again");
                    command.Parameters.AddWithValue("@Twitter", string.Empty);
                    command.Parameters.AddWithValue("@Email", "test@example.com");
                    command.Parameters.AddWithValue("@Blog", string.Empty);
                    command.Parameters.AddWithValue("@FirstName", "Jake");
                    command.Parameters.AddWithValue("@LastName", "Smith");
                    command.Parameters.AddWithValue("@Company", string.Empty);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@Bio", "I like stuff");
                    command.Parameters.AddWithValue("@Twitter", string.Empty);
                    command.Parameters.AddWithValue("@Email", "test@example.com");
                    command.Parameters.AddWithValue("@Blog", string.Empty);
                    command.Parameters.AddWithValue("@FirstName", "Sam");
                    command.Parameters.AddWithValue("@LastName", "Jones");
                    command.Parameters.AddWithValue("@Company", string.Empty);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }

        public static int GetValidSpeakerId()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString))
            {
                connection.Open();
                const string cmdText = "select max(speakerId) from speakers";
                using (var command = new SqlCommand(cmdText, connection))
                {
                    return command.ExecuteScalar().ToString().AsInt();
                }
            }
        }
    }
}
