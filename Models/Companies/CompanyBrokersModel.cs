using System;
using System.Collections.Generic;
using NTBrokers.Models.Brokers;

namespace NTBrokers.Models.Companies
{
    public class CompanyBrokersModel
    {
        public string CompanyName { get; set; }
        public List<BrokerModel> Brokers { get; set; }
        public FilterSortModel FilterSort { get; set; } = new FilterSortModel();
    }
}
