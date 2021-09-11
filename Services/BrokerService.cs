using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NTBrokers.DAL;
using NTBrokers.DAL.Entities;
using NTBrokers.Models.Apartments;
using NTBrokers.Models.Brokers;

namespace NTBrokers.Services
{
    public class BrokerService
    {
        private readonly UnitOfWork _unitOfWork;

        public BrokerService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Broker>> GetAllAsync()
        {
            return await _unitOfWork.BrokerRepository.GetAllAsync();
        }

        public async Task InsertAsync(Broker model)
        {
            await _unitOfWork.BrokerRepository.InsertAsync(model);
            await _unitOfWork.SaveAsync();
        }

        public async Task<BrokerApartmentsModel> GetBrokerApartmentsAsync(int brokerId)
        {
            List<Apartment> apartments = await _unitOfWork.ApartmentRepository.GetAllAsync();
            apartments.Where(x => x.BrokerId == brokerId).ToList();
            List<Broker> brokers = await _unitOfWork.BrokerRepository.GetAllAsync();
            return new()
            {
                Broker = brokers.Where(x => x.BrokerId == brokerId).FirstOrDefault(),
                Apartments = apartments,
                SelectApartments = apartments.Select(x => x.City).Distinct().ToList()
            };
        }

        public async Task<AddApartmentModel> AddApartmentAsync(int brokerId)
        {
            List<CompanyBroker> companyBrokers = await _unitOfWork.CompanyBrokerRepository.GetAllAsync();
            List<int> companiesIds = companyBrokers.Select(x => x.CompanyId).ToList();

            List<Company> companies = await _unitOfWork.CompanyRepository.GetAllAsync();                                   
            List<string> brokerCompaniesNames = companies.Where(company => companiesIds.Contains(company.CompanyId)).ToList()
                                                         .Select(x => x.CompanyName).ToList();

            List<ApartmentModel> apartments = await _unitOfWork.ApartmentRepository.GetAllApartmentsInfoAsync();

            return new()
            {
                BrokerId = brokerId,
                Apartments = apartments.Where(a => string.IsNullOrEmpty(a.BrokerId.ToString())).ToList()
                                       .Where(b => brokerCompaniesNames.Contains(b.CompanyName)).ToList()
            };
        }

        public async Task<BrokerApartmentsModel> FilterAsync(BrokerApartmentsModel model, int brokerId, string broker)
        {
            List<Apartment> apartments = await _unitOfWork.ApartmentRepository.GetAllAsync();
            List<Broker> brokers = await _unitOfWork.BrokerRepository.GetAllAsync();
            return new()
            {
                Broker = brokers.Where(x => x.BrokerId == brokerId).FirstOrDefault(),
                Apartments = apartments.Where(x => x.BrokerId == brokerId).ToList().Where(x => x.City.Contains(model.FilterSort.FilterCity)).ToList(),
                SelectApartments = apartments.Select(x => x.City).Distinct().ToList(),
                BrokerName = broker
            };
        }

        internal async Task UpdateApartmentAsync(int brokerId, int apartmentId)
        {
            List<Apartment> apartments = await _unitOfWork.ApartmentRepository.GetAllAsync();
            Apartment apartment = apartments.Where(x => x.Id == apartmentId).FirstOrDefault();
            apartment.BrokerId = brokerId;
            _unitOfWork.ApartmentRepository.Update(apartment);
            await _unitOfWork.SaveAsync();
        }
    }
}
