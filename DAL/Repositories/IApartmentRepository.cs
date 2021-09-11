using System.Collections.Generic;
using System.Threading.Tasks;
using NTBrokers.DAL.Entities;
using NTBrokers.Models.Apartments;

namespace NTBrokers.DAL.Repositories
{
    public interface IApartmentRepository : IGenericRepository<Apartment>
    {
        Task<List<ApartmentModel>> GetAllApartmentsInfoAsync();
    }
}
