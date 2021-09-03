using System;
using System.Collections.Generic;

namespace NTBrokers.DAL.Entities
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Address { get; set; }

        public ICollection<CompanyBroker> Brokers { get; set; }
    }
}
