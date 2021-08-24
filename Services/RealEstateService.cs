using System;
using NTBrokers.Models;

namespace NTBrokers.Services
{
    public class RealEstateService
    {
        private readonly ApartmentDBService _apartmentDBService;
        private readonly CompanyDBService _companyDBService;
        private readonly BrokerDBService _brokerDBService;

        public RealEstateService(ApartmentDBService apartmentDBService,
                                         CompanyDBService companyDBService,
                                         BrokerDBService brokerDBService)
        {
            _apartmentDBService = apartmentDBService;
            _companyDBService = companyDBService;
            _brokerDBService = brokerDBService;
        }

        public RealEstateModel GetGeneralDBData()
        {
            RealEstateModel model = new();
            model.Apartments = _apartmentDBService.Read();
            model.Companies = _companyDBService.Read();
            model.Brokers =_brokerDBService.Read();

            return model;
        }
    }
}
