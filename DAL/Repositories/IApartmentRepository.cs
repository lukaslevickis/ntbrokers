using System.Collections.Generic;
using NTBrokers.DAL.Entities;
using NTBrokers.Models.Apartments;

namespace NTBrokers.DAL.Repositories
{
    public interface IApartmentRepository : IGenericRepository<Apartment>
    {
        List<ApartmentModel> GetAllApartmentsInfo();
    }
}
