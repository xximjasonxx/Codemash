using System;
using System.Configuration;
using System.Data.SqlClient;
using Codemash.Server.Core.Extensions;

namespace FunctionalTests.Factory
{
    public static class SessionTestDataFactory
    {
        public static void CreateStandardTestData()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString))
            {
                connection.Open();

                // create the speakers
                CreateStandardSpeaker(connection);

                // get the speaker id
                int speakerId = GetLastSpeakerId(connection);

                // create test session data
                CreateStandardSessions(speakerId, connection);
            }
        }

        public static void CreateStandardSpeaker(SqlConnection connection)
        {
            const string cmdText = "insert into Speakers(SpeakerId, Biography, Twitter, EmailAddress, BlogUrl, Name) values(1, @Bio, @Twitter, @Email, @Blog, @Name)";
            using (var command = new SqlCommand(cmdText, connection))
            {
                command.Parameters.AddWithValue("@Bio", "This is a Biography");
                command.Parameters.AddWithValue("@Twitter", string.Empty);
                command.Parameters.AddWithValue("@Email", "test@example.com");
                command.Parameters.AddWithValue("@Blog", string.Empty);
                command.Parameters.AddWithValue("@Name", "Name");
                command.ExecuteNonQuery();
            }
        }

        public static int GetLastSpeakerId(SqlConnection connection)
        {
            const string cmdText = "select max(SpeakerId) from Speakers";
            using (var command = new SqlCommand(cmdText, connection))
            {
                return command.ExecuteScalar().ToString().AsInt();
            }
        }

        private static void CreateStandardSessions(int speakerId, SqlConnection connection)
        {
            const string cmdText = "insert into Sessions(SessionId, SpeakerId, Title, Abstract, Start, [End], Level, Room, Track) values(@SessionId, @SpkId, @Title, @Abs, @Start, @End, 1, 1, 1)";
            using (var command = new SqlCommand(cmdText, connection))
            {
                // session 1
                command.Parameters.AddWithValue("@SessionId", 1);
                command.Parameters.AddWithValue("@SpkId", speakerId);
                command.Parameters.AddWithValue("@Title", "Title 1");
                command.Parameters.AddWithValue("@Abs", ".NET");
                command.Parameters.AddWithValue("@Start", DateTime.Now);
                command.Parameters.AddWithValue("@End", DateTime.Now.AddHours(1));
                command.ExecuteNonQuery();
                command.Parameters.Clear();

                // session 2
                command.Parameters.AddWithValue("@SessionId", 2);
                command.Parameters.AddWithValue("@SpkId", speakerId);
                command.Parameters.AddWithValue("@Title", "Title 2");
                command.Parameters.AddWithValue("@Abs", "LINQ");
                command.Parameters.AddWithValue("@Start", DateTime.Now);
                command.Parameters.AddWithValue("@End", DateTime.Now.AddHours(1));
                command.ExecuteNonQuery();
                command.Parameters.Clear();

                // session 3
                command.Parameters.AddWithValue("@SessionId", 3);
                command.Parameters.AddWithValue("@SpkId", speakerId);
                command.Parameters.AddWithValue("@Title", "Title 3");
                command.Parameters.AddWithValue("@Abs", "Mobile");
                command.Parameters.AddWithValue("@Start", DateTime.Now);
                command.Parameters.AddWithValue("@End", DateTime.Now.AddHours(1));
                command.ExecuteNonQuery();
            }
        }
    }
}
