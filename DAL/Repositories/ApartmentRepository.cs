using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using NTBrokers.Helpers;
using NTBrokers.Models.Apartments;

namespace NTBrokers.DAL.Repositories
{
    public class ApartmentRepository
    {
        private readonly DapperContext _context;

        public ApartmentRepository(DapperContext context)
        {
            _context = context;
        }

        public List<ApartmentModel> GetAll()//todo possible dublicate
        {
            var query = $"SELECT dbo.House.ID, dbo.House.City, dbo.House.Street, dbo.House.Address, " +
                        "dbo.House.FlatFloor, dbo.House.BuildingFloors, dbo.House.Area, dbo.House.BrokerId, " +
                        "dbo.House.CompanyId, dbo.Company.CompanyName, dbo.Broker.Name, dbo.Broker.Surname " +
                        "FROM dbo.House " +
                        "LEFT OUTER JOIN dbo.Company " +
                        "ON dbo.House.CompanyId = dbo.Company.ID " +
                        "LEFT OUTER JOIN dbo.Broker " +
                        "ON dbo.Broker.ID = dbo.House.BrokerId;";

            using (var connection = _context.CreateConnection())
            {
                var items = connection.Query<ApartmentModel>(query);
                return items.ToList();
            }
        }

        public void Create(ApartmentModel model)
        {
            string query = $"INSERT INTO dbo.House (City, Street, Address, FlatFloor, " +
                                               $"BuildingFloors, Area, BrokerId, CompanyId) " +
                                               $"values ('{model.City}', '{model.Street}', '{model.Address}'," +
                                               $"'{model.FlatFloor}', '{model.BuildingFloors}', '{model.Area}'," +
                                               $"null, '{model.CompanyId}');";

            ConnectionsHelpers.ExecuteQuery(query, _context);
        }
    }
}
