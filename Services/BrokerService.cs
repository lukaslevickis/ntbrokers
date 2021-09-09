using System.Collections.Generic;
using System.Linq;
using NTBrokers.DAL;
using NTBrokers.DAL.Entities;
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

        public List<Broker> GetAll()
        {
            return _unitOfWork.BrokerRepository.GetAll().ToList();
        }

        public void Insert(Broker model)
        {
            _unitOfWork.BrokerRepository.Insert(model);
            _unitOfWork.BrokerRepository.Save();
        }

        public BrokerApartmentsModel GetBrokerApartments(int brokerId)
        {
            List<Apartment> apartments = _unitOfWork.ApartmentRepository.GetAll().Where(x => x.BrokerId == brokerId).ToList();
            return new()
            {
                Broker = _unitOfWork.BrokerRepository.GetAll().Where(x => x.BrokerId == brokerId).FirstOrDefault(),
                Apartments = apartments,
                SelectApartments = apartments.Select(x => x.City).Distinct().ToList()
            };
        }

        public AddApartmentModel AddApartment(int brokerId)
        {
            List<int> companiesIds = _unitOfWork.CompanyBrokerRepository.GetAll()
                                                                      .Select(x => x.CompanyId).ToList();

            List<string> brokerCompaniesNames = _unitOfWork.CompanyRepository
                                                           .GetAll()
                                                           .Where(company => companiesIds.Contains(company.CompanyId)).ToList()
                                                           .Select(x => x.CompanyName).ToList();

            return new()
            {
                BrokerId = brokerId,
                Apartments = _unitOfWork.ApartmentRepository
                                        .GetAllApartmentsInfo().Where(a => string.IsNullOrEmpty(a.BrokerId.ToString())).ToList()
                                        .Where(b => brokerCompaniesNames.Contains(b.CompanyName)).ToList()
            };
        }

        public BrokerApartmentsModel Filter(BrokerApartmentsModel model, int brokerId, string broker)
        {
            List<Apartment> apartments = _unitOfWork.ApartmentRepository.GetAll().Where(x => x.BrokerId == brokerId).ToList();
            return new()
            {
                Broker = _unitOfWork.BrokerRepository.GetAll().Where(x => x.BrokerId == brokerId).FirstOrDefault(),
                Apartments = apartments.Where(x => x.City.Contains(model.FilterSort.FilterCity)).ToList(),
                SelectApartments = apartments.Select(x => x.City).Distinct().ToList(),
                BrokerName = broker
            };
        }

        internal void UpdateApartment(int brokerId, int apartmentId)
        {
            List<Apartment> apartments = _unitOfWork.ApartmentRepository.GetAll();
            Apartment apartment = apartments.Where(x => x.Id == apartmentId).FirstOrDefault();
            apartment.BrokerId = brokerId;
            _unitOfWork.ApartmentRepository.Update(apartment);
            _unitOfWork.ApartmentRepository.Save();
        }
    }
}
