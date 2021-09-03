using System;
using System.Collections.Generic;

namespace NTBrokers.DAL.Entities
{
    public class Broker
    {
        public int BrokerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public ICollection<CompanyBroker> Companies { get; set; }
    }
}
