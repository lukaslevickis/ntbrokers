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
        private readonly ApplicationDbContext _context;

        public ApartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<ApartmentModel> GetAll()
        {
            IQueryable<ApartmentModel> result =
                        from apartment in _context.Apartments
                        join broker in _context.Brokers on apartment.BrokerId equals broker.BrokerId into a
                        from broker in a.DefaultIfEmpty()
                        join company in _context.Companies on apartment.CompanyId equals company.CompanyId into b
                        from company in b.DefaultIfEmpty()
                        select new ApartmentModel
                        {
                            Id = apartment.Id,
                            City = apartment.City,
                            Street = apartment.Street,
                            Address = apartment.Address,
                            FlatFloor = apartment.FlatFloor,
                            BuildingFloors = apartment.BuildingFloors,
                            Area = apartment.Area,
                            BrokerId = apartment.BrokerId,
                            CompanyId = apartment.CompanyId,
                            Name = broker.Name,
                            Surname = broker.Surname,
                            CompanyName = company.CompanyName
                        };

            return result;
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
