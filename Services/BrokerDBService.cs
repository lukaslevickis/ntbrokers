using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using NTBrokers.Models;

namespace NTBrokers.Services
{
    public class BrokerDBService
    {
        private readonly SqlConnection _connection;

        public BrokerDBService(SqlConnection connection)
        {
            _connection = connection;
        }

        public List<BrokerModel> Read()
        {
            List<BrokerModel> items = new();

            _connection.Open();

            using var command = new SqlCommand("SELECT ID, Name, Surname FROM dbo.Broker;", _connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                items.Add(
                new BrokerModel
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Surname = reader.GetString(2),
                });
            }

            _connection.Close();

            return items;
        }

        public void Create(BrokerModel model)
        {
            _connection.Open();

            using var command = new SqlCommand($"INSERT into dbo.Broker (Name, Surname) values ('{model.Name}', '{model.Surname}');", _connection);
            command.ExecuteNonQuery();

            _connection.Close();
        }
    }
}
