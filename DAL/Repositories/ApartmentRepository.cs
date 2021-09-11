using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NTBrokers.DAL.Entities;
using NTBrokers.Models.Apartments;

namespace NTBrokers.DAL.Repositories
{
    public class ApartmentRepository: GenericRepository<Apartment>, IApartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public ApartmentRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public async Task<List<ApartmentModel>> GetAllApartmentsInfoAsync()
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
                            BrokerId = apartment.BrokerId,          //todo linq method syntax
                            CompanyId = apartment.CompanyId,
                            Name = broker.Name,
                            Surname = broker.Surname,
                            CompanyName = company.CompanyName
                        };

            return await result.ToListAsync();
        }
    }
}
