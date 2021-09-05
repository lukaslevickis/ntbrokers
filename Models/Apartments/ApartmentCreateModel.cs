using System.Linq;
using NTBrokers.DAL.Entities;

namespace NTBrokers.Models.Apartments
{
    public class ApartmentCreateModel
    {
        public Apartment Apartment { get; set; } = new Apartment();
        public IQueryable<Company> Companies { get; set; }
    }
}
