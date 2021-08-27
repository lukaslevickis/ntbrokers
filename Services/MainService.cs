using System;
namespace NTBrokers.Services
{
    public class MainService
    {
        public readonly ApartmentDBService _apartmentDBService;
        public readonly CompanyDBService _companyDBService;
        public readonly BrokerDBService _brokerDBService;

        public MainService(ApartmentDBService apartmentDBService,
                                         CompanyDBService companyDBService,
                                         BrokerDBService brokerDBService)
        {
            _apartmentDBService = apartmentDBService;
            _companyDBService = companyDBService;
            _brokerDBService = brokerDBService;
        }

        //lists todo DbSet<Developer> Developers
    }
}
