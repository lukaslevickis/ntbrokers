using System;
using System.Collections.Generic;
using NTBrokers.DAL.Entities;
using NTBrokers.Models.Apartments;

namespace NTBrokers.Models.Brokers
{
    public class BrokerApartmentsModel
    {
        public Broker Broker { get; set; }
        public int BrokerId { get; set; }
        public string BrokerName { get; set; }
        public List<ApartmentModel> Apartments { get; set; }
        public List<string> SelectApartments { get; set; }
        public FilterSortModel FilterSort { get; set; } = new FilterSortModel();
    }
}
