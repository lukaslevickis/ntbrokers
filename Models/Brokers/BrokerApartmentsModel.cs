using System.Collections.Generic;
using System.Linq;
using NTBrokers.DAL.Entities;

namespace NTBrokers.Models.Brokers
{
    public class BrokerApartmentsModel
    {
        public Broker Broker { get; set; }
        public int BrokerId { get; set; }
        public string BrokerName { get; set; }
        public IQueryable<Apartment> Apartments { get; set; }
        public List<string> SelectApartments { get; set; }
        public FilterSortModel FilterSort { get; set; } = new FilterSortModel();
    }
}
