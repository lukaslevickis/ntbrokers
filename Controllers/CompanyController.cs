using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NTBrokers.DAL;
using NTBrokers.Models.Brokers;
using NTBrokers.Models.Companies;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NTBrokers.Controllers
{
    public class CompanyController : Controller
    {
        private UnitOfWork _unitOfWork;

        public CompanyController(DapperContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            CompanyModel company = new();
            return View(_unitOfWork.CompanyRepository.GetAll(company.TableName));
        }

        public IActionResult Create()
        {
            BrokerModel broker = new();
            CompanyCreateModel data = new CompanyCreateModel()
            {
                Brokers = _unitOfWork.BrokerRepository.GetAll(broker.TableName),
                Company = new CompanyModel()
            };

            return View(data);
        }

        public IActionResult Submit(CompanyCreateModel model)
        {
            CompanyModel company = new();
            _unitOfWork.CustomCompanyRepository.Create(model);
            return View("Index", _unitOfWork.CompanyRepository.GetAll(company.TableName));
        }

        public IActionResult CompanyBrokers(int companyId)
        {
            BrokerModel broker = new();
            List<int> brokersIds = _unitOfWork.CompanyBrokerRepository.GetByID("CompanyBroker", "CompanyId", companyId)
                                                                      .Select(x => x.BrokerId).ToList();
            
            return View(_unitOfWork.BrokerRepository.GetAll(broker.TableName)
                                                    .Where(x => brokersIds.Contains(x.Id)).ToList());
        }

        public IActionResult Edit(int companyId)
        {
            BrokerModel broker = new();
            List<int> brokersIds = _unitOfWork.CompanyBrokerRepository.GetByID("CompanyBroker", "CompanyId", companyId)
                                                                      .Select(x => x.BrokerId).ToList();

            List<BrokerModel> brokers = _unitOfWork.BrokerRepository.GetAll(broker.TableName);
            CompanyModel company = _unitOfWork.CompanyRepository.GetByID("Company", "ID", companyId).ToList().FirstOrDefault();
            company.Id = companyId;

            CompanyCreateModel data = new CompanyCreateModel()
            {
                Brokers = brokers,
                SelectedBrokers = brokers.Where(x => brokersIds.Contains(x.Id)).ToList(),
                Company = _unitOfWork.CompanyRepository.GetByID("Company", "ID", companyId).ToList().FirstOrDefault()
            };

            return View(data);
        }

        public IActionResult Update(CompanyCreateModel model)
        {
            CompanyModel company = new();
            _unitOfWork.CustomCompanyRepository.UpdateCompany(model);
            List<string> existingBrokers = _unitOfWork.CompanyBrokerRepository.GetByID("CompanyBroker", "CompanyId", model.Company.Id)
                                                                      .Select(x => x.BrokerId.ToString()).ToList();

            _unitOfWork.CustomCompanyRepository.UpdateRemoveCompanyBrokers(model, existingBrokers);
            return View("Index", _unitOfWork.CompanyRepository.GetAll(company.TableName));
        }
    }
}
