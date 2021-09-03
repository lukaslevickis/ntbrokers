using System;
namespace NTBrokers.DAL.Entities
{
    public class CompanyBroker
    {
        public int BrokerId { get; set; }
        public Broker Broker { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
