using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using NTBrokers.Helpers;
using NTBrokers.Models;
using NTBrokers.Models.Brokers;
using NTBrokers.Models.Companies;

namespace NTBrokers.Services
{
    public class CompanyDBService
    {
        private readonly SqlConnection _connection;

        public CompanyDBService(SqlConnection connection)
        {
            _connection = connection;

        }

        public List<CompanyModel> GetAll()
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

        public void Create(CompanyCreateModel model)
        {
            string query = $"INSERT into dbo.Company (Name, City, Street, Address) values ('{model.Company.Name}', " +
                           $"'{model.Company.City}', '{model.Company.Street}', '{model.Company.Address}');";

            ConnectionsHelpers.ExecuteQuery(query, _connection);


            ////////////todo
            List<CompanyModel> id = GetCompanyId();
            int companyId = id.LastOrDefault().Id;

            string query2 = "";
            foreach (string brokerId in model.CreateFormSelectedBrokers)
            {
                query2 += $"INSERT into dbo.CompanyBroker (BrokerId, CompanyId) values ({brokerId}, {companyId});";
            }

            ConnectionsHelpers.ExecuteQuery(query2, _connection);
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

        internal List<BrokerModel> CompanyBrokers(int companyId)//todo code dublication
        {
            List<BrokerModel> brokers = new List<BrokerModel>();
            List<int> brokersIds = new List<int>();

            _connection.Open();

            using var command = new SqlCommand($"SELECT * FROM dbo.CompanyBroker WHERE CompanyId = {companyId}", _connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                brokersIds.Add(reader.GetInt32(0));
            }

            _connection.Close();

            return brokers;// _mainService._brokerDBService.GetAll().Where(x => brokersIds.Contains(x.Id)).ToList();
        }
    }
}
