using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using NTBrokers.Models;

namespace NTBrokers.Services
{
    public class ApartmentDBService
    {
        private readonly SqlConnection _connection;

        public ApartmentDBService(SqlConnection connection)
        {
            _connection = connection;
        }

        internal List<ApartmentAdditionalModel> Read()
        {
            List<ApartmentAdditionalModel> items = new();

            _connection.Open();

            //many to many left joins
            using var command = new SqlCommand("SELECT dbo.House.ID, dbo.House.City, dbo.House.Street, dbo.House.Address, " +
                                               "dbo.House.FlatFloor, dbo.House.BuildingFloors, dbo.House.Area, dbo.House.BrokerId, " +
                                               "dbo.House.CompanyId, dbo.Company.Name, dbo.Broker.Name, dbo.Broker.Surname " +
                                                "FROM dbo.House " +
                                               //"LEFT OUTER JOIN dbo.CompanyBroker " +
                                               // "ON dbo.House.BrokerId = dbo.CompanyBroker.BrokerId " +
                                               // "LEFT OUTER JOIN dbo.Broker " +
                                               //  "ON dbo.CompanyBroker.BrokerId = dbo.Broker.ID " +
                                               //"LEFT OUTER JOIN dbo.Company " +
                                               // "ON dbo.CompanyBroker.CompanyId = dbo.Company.ID;", _connection);
                                               "LEFT OUTER JOIN dbo.Company " +
                                                "ON dbo.House.CompanyId = dbo.Company.ID " +
                                               "LEFT OUTER JOIN dbo.Broker " +
                                                "ON dbo.Broker.ID = dbo.House.BrokerId;", _connection);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                items.Add(
                new ApartmentAdditionalModel
                {
                    //Id = reader.GetInt32(0),
                    //City = reader.GetString(1),
                    //Street = reader.GetString(2),
                    //Address = reader.GetInt32(3),
                    //FlatFloor = reader.GetInt32(4),
                    //BuildingFloors = reader.GetInt32(5),
                    //Area = reader.GetInt32(6),
                    //BrokerId = reader.GetInt32(7),
                    //CompanyId = reader.GetInt32(8),
                    Apartment = reader.GetString(2) + " " + reader.GetInt32(3),
                    Company = reader.GetString(9),
                    Broker = reader.GetString(10) + " " + reader.GetString(11),

                });
            }

            _connection.Close();

            return items;
        }

        public void Create(RealEstateModel model)
        {
            _connection.Open();
            using var command = new SqlCommand($"INSERT INTO dbo.House2 (City, Street, Address, FlatFloor, " +
                                               $"BuildingFloors, Area, BrokerId, CompanyId) " +
                                               $"values ('{model.Apartment.City}', '{model.Apartment.Street}', '{model.Apartment.Address}'," +
                                               $"'{model.Apartment.FlatFloor}', '{model.Apartment.BuildingFloors}', '{model.Apartment.Area}'," +
                                               $"null, '{model.Apartment.CompanyId}');", _connection);
            command.ExecuteNonQuery();
            _connection.Close();

            //_connection.Open();
            //using var command2 = new SqlCommand($"INSERT INTO dbo.CompanyBroker (BrokerId, CompanyId) " +
            //                                    $"values ('{model.Apartment.BrokerId}', '{model.Apartment.CompanyId}');", _connection);
            //command2.ExecuteNonQuery();
            //_connection.Close();
        }
    }
}
