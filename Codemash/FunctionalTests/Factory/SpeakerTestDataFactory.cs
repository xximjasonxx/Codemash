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
                const string cmdText = "insert into Speakers(SpeakerId, Biography, Twitter, EmailAddress, BlogUrl, Name) values(@SpeakerId, @Bio, @Twitter, @Email, @Blog, @Name)";
                using (var command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("@SpeakerId", 1);
                    command.Parameters.AddWithValue("@Bio", "My Biography");
                    command.Parameters.AddWithValue("@Twitter", string.Empty);
                    command.Parameters.AddWithValue("@Email", "test@example.com");
                    command.Parameters.AddWithValue("@Blog", string.Empty);
                    command.Parameters.AddWithValue("@Name", "Troy Smith");
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@SpeakerId", 2);
                    command.Parameters.AddWithValue("@Bio", "My Biography Again");
                    command.Parameters.AddWithValue("@Twitter", string.Empty);
                    command.Parameters.AddWithValue("@Email", "test@example.com");
                    command.Parameters.AddWithValue("@Blog", string.Empty);
                    command.Parameters.AddWithValue("@Name", "Jake Smith");
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@SpeakerId", 3);
                    command.Parameters.AddWithValue("@Bio", "I like stuff");
                    command.Parameters.AddWithValue("@Twitter", string.Empty);
                    command.Parameters.AddWithValue("@Email", "test@example.com");
                    command.Parameters.AddWithValue("@Blog", string.Empty);
                    command.Parameters.AddWithValue("@Name", "Sam Jones");
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
