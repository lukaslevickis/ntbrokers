using System.Collections.Generic;
using NTBrokers.DAL.Entities;

namespace NTBrokers.Models.Companies
{
    public class CompanyBrokersModel
    {
        public string CompanyName { get; set; }
        public List<Broker> Brokers { get; set; }
        public FilterSortModel FilterSort { get; set; } = new FilterSortModel();
    }
}
