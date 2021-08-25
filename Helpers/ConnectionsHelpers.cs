using System;
using Microsoft.Data.SqlClient;

namespace NTBrokers.Helpers
{
    public static class ConnectionsHelpers
    {
        public static void ExecuteQuery(string query, SqlConnection connection)
        {
            connection.Open();

            using var command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}
