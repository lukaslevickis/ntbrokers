using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task InsertCompanyBrokerAsync(CompanyCreateModel model)
        {
            int companyId = model.Company.CompanyId;
            foreach (int brokerId in model.CreateFormSelectedBrokers)
            {
                CompanyBroker companyBroker = new() { BrokerId = brokerId, CompanyId = companyId };
                await _unitOfWork.CompanyBrokerRepository.InsertAsync(companyBroker);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task UpdateCompanyBrokersAsync(CompanyCreateModel model)
        {
            _unitOfWork.CompanyRepository.Update(model.Company);
            await _unitOfWork.SaveAsync();
            List<CompanyBroker> companyBrokers = await _unitOfWork.CompanyBrokerRepository.GetAllAsync();
            List<int> existingBrokers = companyBrokers.Where(x => x.CompanyId == model.Company.CompanyId).Select(b => b.BrokerId).ToList();

            GetCompanyBrokersID(model.CreateFormSelectedBrokers, existingBrokers,
                                out List<int> brokersToRemove, out List<int> brokersToAdd);


            if (brokersToAdd != null)
            {
                foreach (int brokerId in brokersToAdd)
                {
                    CompanyBroker companyBroker = new() { BrokerId = brokerId, CompanyId = model.Company.CompanyId };
                    await _unitOfWork.CompanyBrokerRepository.InsertAsync(companyBroker);
                    await _unitOfWork.SaveAsync();
                }
            }

            if (brokersToRemove != null)
            {
                foreach (int brokerId in brokersToRemove)
                {
                    await _unitOfWork.CompanyBrokerRepository.DeleteCompanyBrokerAsync(brokerId, model.Company.CompanyId);
                    await _unitOfWork.SaveAsync();
                }
            }
        }

        public async Task<CompanyBrokersModel> SortByAsync(CompanyBrokersModel model, string companyName)
        {
            List<Company> companies = await _unitOfWork.CompanyRepository.GetAllAsync();
            int companyId = companies.Where(x => x.CompanyName == companyName).Select(x => x.CompanyId).FirstOrDefault();

            List<CompanyBroker> companyBrokers = await _unitOfWork.CompanyBrokerRepository.GetAllAsync();
            List<int> brokersIds = companyBrokers.Where(x => x.CompanyId == companyId).Select(b => b.BrokerId).ToList();

            CompanyBrokersModel data = new();
            data.CompanyName = companies.Where(x => x.CompanyId == companyId).Select(b => b.CompanyName).FirstOrDefault();

            List<Broker> brokers = await _unitOfWork.BrokerRepository.GetAllAsync();
            data.Brokers = brokers.Where(x => brokersIds.Contains(x.BrokerId)).ToList();

            data.Brokers = model.FilterSort.SortOrder == "Name" ? data.Brokers.OrderBy(x => x.Name).ToList()
                                                                : data.Brokers.OrderBy(x => x.Surname).ToList();

            data.CompanyName = companyName;

            return data;
        }

        public async Task<CompanyBrokersModel> GetCompanyBrokersAsync(int companyId)
        {
            List<CompanyBroker> companyBrokers = await _unitOfWork.CompanyBrokerRepository.GetAllAsync();
            List<int> brokersIds = companyBrokers.Where(x => x.CompanyId == companyId)
                                                                      .Select(b => b.BrokerId).ToList();

            CompanyBrokersModel data = new();
            List<Company> companies = await _unitOfWork.CompanyRepository.GetAllAsync();
            data.CompanyName = companies.Where(x => x.CompanyId == companyId).Select(b => b.CompanyName).FirstOrDefault();

            List<Broker> brokers = await _unitOfWork.BrokerRepository.GetAllAsync();
            data.Brokers = brokers.Where(x => brokersIds.Contains(x.BrokerId)).ToList();

            return data;
        }

        public async Task<CompanyCreateModel> EditAsync(int companyId)
        {
            List<CompanyBroker> companyBrokers = await _unitOfWork.CompanyBrokerRepository.GetAllAsync();
            List<int> brokersIds = companyBrokers.Where(x => x.CompanyId == companyId).Select(b => b.BrokerId).ToList();

            List<Broker> brokers = await _unitOfWork.BrokerRepository.GetAllAsync();
            return new()
            {
                Brokers = brokers,
                SelectedBrokers = brokers.Where(x => brokersIds.Contains(x.BrokerId)).ToList(),
                Company = await _unitOfWork.CompanyRepository.GetByIDAsync(companyId)
            };
        }

        public async Task InsertAsync(Company company)
        {
            await _unitOfWork.CompanyRepository.InsertAsync(company);
            await _unitOfWork.SaveAsync();
        }

        public async Task<CompanyCreateModel> CreateAsync()
        {
            return new()
            {
                Brokers = await _unitOfWork.BrokerRepository.GetAllAsync(),
                Company = new Company()
            };
        }

        public async Task<List<Company>> GetAllAsync()
        {
            return await _unitOfWork.CompanyRepository.GetAllAsync();
        }
    }
}
