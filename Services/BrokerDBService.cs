using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using NTBrokers.Models;

namespace NTBrokers.Services
{
    public class BrokerDBService
    {
        private readonly SqlConnection _connection;
        private readonly ApartmentDBService _apartmentDBService;
        private readonly CompanyDBService _companyDBService;

        public BrokerDBService(SqlConnection connection, ApartmentDBService apartmentDBService, CompanyDBService companyDBService)
        {
            _connection = connection;
            _apartmentDBService = apartmentDBService;
            _companyDBService = companyDBService;
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

        internal List<ApartmentModel> BrokerApartments(int brokerId)
        {
            return _apartmentDBService.Read().Where(x => x.BrokerId == brokerId).ToList();
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

            List<string> brokerCompaniesNames = _companyDBService.Read()
                                                           .Where(company => companiesIds.Contains(company.Id)).ToList()
                                                           .Select(x => x.Name).ToList();

            return _apartmentDBService.Read()
                                      .Where(a => string.IsNullOrEmpty(a.BrokerId.ToString())).ToList()
                                      .Where(b => brokerCompaniesNames.Contains(b.Company)).ToList();
        }

        internal void SubmitApartment(int brokerId, int apartmentId)
        {
            _connection.Open();

            using var command = new SqlCommand($"update dbo.House2 set BrokerId = {brokerId} WHERE ID = {apartmentId};", _connection);
            command.ExecuteNonQuery();

            _connection.Close();
        }
    }
}
