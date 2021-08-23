using System.Collections.Generic;

namespace NTBrokers.Models
{
    public class RealEstateModel
    {
        public ApartmentModel Apartment { get; set; } = new ApartmentModel();
        public ApartmentAdditionalModel ApartmentAdditionalModel { get; set; } = new ApartmentAdditionalModel();
        public CompanyModel Company { get; set; } = new CompanyModel();
        public CompanyAdditionalModel CompanyAdditionalModel { get; set; } = new CompanyAdditionalModel();

        public List<ApartmentAdditionalModel> Apartments { get; set; } = new List<ApartmentAdditionalModel>();
        public List<BrokerModel> Brokers { get; set; } = new List<BrokerModel>();
        public List<CompanyModel> Companies { get; set; } = new List<CompanyModel>();
        //public FilterSortModel FilterSort { get; set; } = new FilterSortModel();
    }
}
