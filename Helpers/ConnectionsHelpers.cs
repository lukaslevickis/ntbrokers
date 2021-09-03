using System;
using Dapper;
using Microsoft.Data.SqlClient;
using NTBrokers.DAL;

namespace NTBrokers.Helpers
{
    public static class ConnectionsHelpers
    {
        public static void ExecuteQuery(string query, ApplicationDbContext context)
        {
            //using (var connection = context.CreateConnection())
            //{
            //    connection.Execute(query);
            //}
        }
    }
}
