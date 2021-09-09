using System.Collections.Generic;
using NTBrokers.DAL.Entities;

namespace NTBrokers.Models.Apartments
{
    public class ApartmentCreateModel
    {
        public Apartment Apartment { get; set; } = new Apartment();
        public List<Company> Companies { get; set; }
    }
}
