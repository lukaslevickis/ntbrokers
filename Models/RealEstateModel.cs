using System.Collections.Generic;

namespace NTBrokers.Models
{
    public class RealEstateModel
    {
        public ApartmentModel Apartment { get; set; } = new ApartmentModel();
        public CompanyModel Company { get; set; } = new CompanyModel();
        public BrokerModel Broker { get; set; } = new BrokerModel();
        public CompanyAdditionalModel CompanyAdditionalModel { get; set; } = new CompanyAdditionalModel();

        public List<ApartmentModel> Apartments { get; set; } = new List<ApartmentModel>();
        public List<BrokerModel> Brokers { get; set; } = new List<BrokerModel>();
        public List<CompanyModel> Companies { get; set; } = new List<CompanyModel>();
        //public FilterSortModel FilterSort { get; set; } = new FilterSortModel();
    }
}
