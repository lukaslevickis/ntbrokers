using System.Collections.Generic;
using System.Linq;
using NTBrokers.DAL;
using NTBrokers.DAL.Entities;
using NTBrokers.Models.Companies;

namespace NTBrokers.Services
{
    public class CompanyService
    {
        private readonly UnitOfWork _unitOfWork;

        public CompanyService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void GetCompanyBrokersID(int[] createFormSelectedBrokers, List<int> existingBrokers,
                                        out List<int> brokersToRemove, out List<int> brokersToAdd)
        {
            brokersToRemove = null;
            brokersToAdd = null;
            List<int> selectedBrokers = createFormSelectedBrokers?.ToList();
            if (selectedBrokers == null)
            {
                brokersToRemove = existingBrokers;
            }
            else
            {
                brokersToRemove = existingBrokers.Where(x => !selectedBrokers.Contains(x))?.ToList();
                brokersToAdd = selectedBrokers.Where(x => !existingBrokers.Contains(x)).ToList();
            }
        }

        public void InsertCompanyBroker(CompanyCreateModel model)
        {
            int companyId = model.Company.CompanyId;
            foreach (int brokerId in model.CreateFormSelectedBrokers)
            {
                CompanyBroker companyBroker = new() { BrokerId = brokerId, CompanyId = companyId };
                _unitOfWork.CompanyBrokerRepository.Insert(companyBroker);
                _unitOfWork.CompanyBrokerRepository.Save();
            }
        }

        public void UpdateCompanyBrokers(CompanyCreateModel model)
        {
            _unitOfWork.CompanyRepository.Update(model.Company);
            _unitOfWork.CompanyRepository.Save();
            List<int> existingBrokers = _unitOfWork
                                            .CompanyBrokerRepository.GetAll()
                                                                    .Where(x => x.CompanyId == model.Company.CompanyId)
                                                                    .Select(b => b.BrokerId).ToList();

            GetCompanyBrokersID(model.CreateFormSelectedBrokers, existingBrokers,
                                                      out List<int> brokersToRemove, out List<int> brokersToAdd);


            if (brokersToAdd != null)
            {
                foreach (int brokerId in brokersToAdd)
                {
                    CompanyBroker companyBroker = new() { BrokerId = brokerId, CompanyId = model.Company.CompanyId };
                    _unitOfWork.CompanyBrokerRepository.Insert(companyBroker);
                    _unitOfWork.CompanyBrokerRepository.Save();
                }
            }

            if (brokersToRemove != null)
            {
                foreach (int brokerId in brokersToRemove)
                {
                    _unitOfWork.CompanyBrokerRepository.DeleteCompanyBroker(brokerId, model.Company.CompanyId);
                    _unitOfWork.CompanyBrokerRepository.Save();
                }
            }
        }

        public CompanyBrokersModel SortBy(CompanyBrokersModel model, string companyName)
        {
            List<Company> companies = _unitOfWork.CompanyRepository.GetAll();
            int companyId = companies.Where(x => x.CompanyName == companyName).Select(x => x.CompanyId).FirstOrDefault();
            List<int> brokersIds = _unitOfWork.CompanyBrokerRepository.GetAll().Where(x => x.CompanyId == companyId)
                                                                      .Select(b => b.BrokerId).ToList();

            CompanyBrokersModel data = new();
            data.CompanyName = companies.Where(x => x.CompanyId == companyId)
                                                                      .Select(b => b.CompanyName).FirstOrDefault();

            data.Brokers = _unitOfWork.BrokerRepository.GetAll()
                                                       .Where(x => brokersIds.Contains(x.BrokerId)).ToList();

            data.Brokers = model.FilterSort.SortOrder == "Name" ? data.Brokers.OrderBy(x => x.Name).ToList()
                                                                : data.Brokers.OrderBy(x => x.Surname).ToList();

            data.CompanyName = companyName;

            return data;
        }

        public CompanyBrokersModel GetCompanyBrokers(int companyId)
        {
            List<int> brokersIds = _unitOfWork.CompanyBrokerRepository.GetAll().Where(x => x.CompanyId == companyId)
                                                                      .Select(b => b.BrokerId).ToList();

            CompanyBrokersModel data = new();
            data.CompanyName = _unitOfWork.CompanyRepository.GetAll().Where(x => x.CompanyId == companyId)
                                                                      .Select(b => b.CompanyName).FirstOrDefault();

            data.Brokers = _unitOfWork.BrokerRepository.GetAll()
                                                       .Where(x => brokersIds.Contains(x.BrokerId)).ToList();

            return data;
        }

        public CompanyCreateModel Edit(int companyId)
        {
            List<int> brokersIds = _unitOfWork.CompanyBrokerRepository.GetAll().Where(x => x.CompanyId == companyId)
                                                                      .Select(b => b.BrokerId).ToList();

            List<Broker> brokers = _unitOfWork.BrokerRepository.GetAll();
            return new()
            {
                Brokers = brokers,
                SelectedBrokers = brokers.Where(x => brokersIds.Contains(x.BrokerId)).ToList(),
                Company = _unitOfWork.CompanyRepository.GetByID(companyId)
            };
        }

        public void Insert(Company company)
        {
            _unitOfWork.CompanyRepository.Insert(company);
            _unitOfWork.CompanyRepository.Save();
        }

        public CompanyCreateModel Create()
        {
            return new()
            {
                Brokers = _unitOfWork.BrokerRepository.GetAll(),
                Company = new Company()
            };
        }

        public List<Company> GetAll()
        {
            return _unitOfWork.CompanyRepository.GetAll();
        }
    }
}
