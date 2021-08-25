using System;
using System.Collections.Generic;
using NTBrokers.Models.Apartments;

namespace NTBrokers.Models.Brokers
{
    public class BrokerApartmentModel
    {
        public BrokerModel Broker { get; set; }
        public int BrokerId { get; set; }
        public List<ApartmentModel> Apartments { get; set; }
    }
}
