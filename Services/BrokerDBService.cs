using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using NTBrokers.Helpers;
using NTBrokers.Models.Apartments;
using NTBrokers.Models.Brokers;

namespace NTBrokers.Services
{
    public class BrokerDBService
    {
        private readonly SqlConnection _connection;
        private readonly MainService _mainService;

        public BrokerDBService(SqlConnection connection, MainService mainService)
        {
            _connection = connection;
            _mainService = mainService;
        }

        public List<BrokerModel> GetAll()
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
            string query = $"INSERT into dbo.Broker (Name, Surname) values ('{model.Name}', '{model.Surname}');";

            ConnectionsHelpers.ExecuteQuery(query, _connection);
        }

        internal List<ApartmentModel> BrokerApartments(int brokerId)
        {
            return _mainService._apartmentDBService.GetAll().Where(x => x.BrokerId == brokerId).ToList();
        }

        internal List<ApartmentModel> AddApartment(int brokerId)
        {
            //this method gets all available apartments by brokerId

            List<int> companiesIds = new List<int>();

            _connection.Open();

            using var command = new SqlCommand($"SELECT * FROM dbo.CompanyBroker WHERE BrokerId = {brokerId}", _connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                companiesIds.Add(reader.GetInt32(1));
            }

            _connection.Close();

            List<string> brokerCompaniesNames = _mainService._companyDBService.GetAll()
                                                           .Where(company => companiesIds.Contains(company.Id)).ToList()
                                                           .Select(x => x.Name).ToList();

            return _mainService._apartmentDBService.GetAll()
                                      .Where(a => string.IsNullOrEmpty(a.BrokerId.ToString())).ToList()
                                      .Where(b => brokerCompaniesNames.Contains(b.Company)).ToList();
        }

        internal void SubmitApartment(int brokerId, int apartmentId)
        {
            string query = $"update dbo.House2 set BrokerId = {brokerId} WHERE ID = {apartmentId};";

            ConnectionsHelpers.ExecuteQuery(query, _connection);
        }
    }
}
