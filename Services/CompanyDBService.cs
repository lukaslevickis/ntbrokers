using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using NTBrokers.Models;

namespace NTBrokers.Services
{
    public class CompanyDBService
    {
        private readonly SqlConnection _connection;

        public CompanyDBService(SqlConnection connection)
        {
            _connection = connection;
        }

        public List<CompanyModel> Read()
        {
            List<CompanyModel> items = new();

            _connection.Open();

            using var command = new SqlCommand("SELECT ID, Name, City, Street, Address FROM dbo.Company;", _connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                items.Add(
                new CompanyModel
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    City = reader.GetString(2),
                    Street = reader.GetString(3),
                    Address = reader.GetInt32(4),
                });
            }

            _connection.Close();

            return items;
        }

        public void Create(RealEstateModel model)
        {
            _connection.Open();

            using var command = new SqlCommand($"INSERT into dbo.Company (Name, City, Street, Address) values ('{model.Company.Name}', '{model.Company.City}', '{model.Company.Street}', '{model.Company.Address}');", _connection);
            command.ExecuteNonQuery();

            _connection.Close();

            List<CompanyModel> id = GetCompanyId();
            int companyId = id.LastOrDefault().Id;

            string sqlQuery = "";
            foreach (string brokerId in model.CompanyAdditionalModel.CreateFormSelectedBrokers)
            {
                sqlQuery += $"INSERT into dbo.CompanyBroker (BrokerId, CompanyId) values ({brokerId}, {companyId});";
            }

            _connection.Open();

            using var command2 = new SqlCommand(sqlQuery, _connection);
            command2.ExecuteNonQuery();

            _connection.Close();
        }

        public List<CompanyModel> GetCompanyId()
        {
            List<CompanyModel> items = new();

            _connection.Open();

            using var command = new SqlCommand("SELECT dbo.Company.ID from dbo.Company", _connection);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                items.Add(
                new CompanyModel
                {
                    Id = reader.GetInt32(0),
                });
            }

            _connection.Close();

            return items;
        }
    }
}
