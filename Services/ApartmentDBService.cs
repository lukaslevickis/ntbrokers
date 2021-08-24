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

        internal List<ApartmentModel> Read()
        {
            List<ApartmentModel> items = new();

            _connection.Open();

            //many to many left joins
            using var command = new SqlCommand("SELECT dbo.House2.ID, dbo.House2.City, dbo.House2.Street, dbo.House2.Address, " +
                                               "dbo.House2.FlatFloor, dbo.House2.BuildingFloors, dbo.House2.Area, dbo.House2.BrokerId, " +
                                               "dbo.House2.CompanyId, dbo.Company.Name, dbo.Broker.Name, dbo.Broker.Surname " +
                                                "FROM dbo.House2 " +
                                               "LEFT OUTER JOIN dbo.Company " +
                                                "ON dbo.House2.CompanyId = dbo.Company.ID " +
                                               "LEFT OUTER JOIN dbo.Broker " +
                                                "ON dbo.Broker.ID = dbo.House2.BrokerId;", _connection);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                items.Add(
                new ApartmentModel
                {
                    Id = reader.GetInt32(0),
                    City = reader.GetString(1),
                    Street = reader.GetString(2),
                    Address = reader.GetString(3),
                    FlatFloor = reader.GetInt32(4),
                    BuildingFloors = reader.GetInt32(5),
                    Area = reader.GetInt32(6),
                    BrokerId = reader.IsDBNull(7) ? null : reader.GetInt32(7),
                    CompanyId = reader.GetInt32(8),
                    Apartment = reader.GetString(2) + " " + reader.GetString(3),
                    Company = reader.GetString(9),
                    Broker = (reader.IsDBNull(10) ? null : reader.GetString(10)) + " " + (reader.IsDBNull(10) ? null : reader.GetString(11)),

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
        }
    }
}
