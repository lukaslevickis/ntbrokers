using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NTBrokers.DAL;
using NTBrokers.DAL.Entities;
using NTBrokers.Models.Companies;
using NTBrokers.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NTBrokers.Controllers
{
    public class CompanyController : Controller
    {
        private UnitOfWork _unitOfWork;
        private CompanyBrokerService _companyBrokerService;

        public CompanyController(ApplicationDbContext context, CompanyBrokerService companyBrokerService)
        {
            _unitOfWork = new UnitOfWork(context);
            _companyBrokerService = companyBrokerService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_unitOfWork.CompanyRepository.GetAll());
        }

        public IActionResult Create()
        {
            CompanyCreateModel data = new()
            {
                Brokers = _unitOfWork.BrokerRepository.GetAll(),
                Company = new Company()
            };

            return View(data);
        }

        public IActionResult Submit(CompanyCreateModel model)
        {
            _unitOfWork.CustomCompanyRepository.InsertCompany(model.Company);
            _unitOfWork.CustomCompanyRepository.Save();
            int companyId = model.Company.CompanyId;
            foreach (int brokerId in model.CreateFormSelectedBrokers)
            {
                CompanyBroker companyBroker = new() { BrokerId = brokerId, CompanyId = companyId };
                _unitOfWork.CustomCompanyBrokerRepository.InsertCompanyBroker(companyBroker);
                _unitOfWork.CustomCompanyBrokerRepository.Save();
            }

            return View("Index", _unitOfWork.CompanyRepository.GetAll());
        }

        public IActionResult CompanyBrokers(int companyId)
        {
            List<int> brokersIds = _unitOfWork.CompanyBrokerRepository.GetAll().Where(x => x.CompanyId == companyId)
                                                                      .Select(b => b.BrokerId).ToList();

            CompanyBrokersModel data = new();
            data.CompanyName = _unitOfWork.CompanyRepository.GetAll().Where(x => x.CompanyId == companyId)
                                                                      .Select(b => b.CompanyName).FirstOrDefault();

            data.Brokers = _unitOfWork.BrokerRepository.GetAll()
                                                       .Where(x => brokersIds.Contains(x.BrokerId));
            return View(data);
        }

        public IActionResult Edit(int companyId)
        {
            List<int> brokersIds = _unitOfWork.CompanyBrokerRepository.GetAll().Where(x => x.CompanyId == companyId)
                                                                      .Select(b => b.BrokerId).ToList();

            IQueryable<Broker> brokers = _unitOfWork.BrokerRepository.GetAll();
            CompanyCreateModel data = new()
            {
                Brokers = brokers,
                SelectedBrokers = brokers.Where(x => brokersIds.Contains(x.BrokerId)).ToList(),
                Company = _unitOfWork.CompanyRepository.GetByID(companyId)
            };

            return View(data);
        }

        public IActionResult Update(CompanyCreateModel model)
        {
            _unitOfWork.CompanyRepository.Update(model.Company);
            _unitOfWork.CompanyRepository.Save();
            List<int> existingBrokers = _unitOfWork
                                            .CompanyBrokerRepository.GetAll()
                                                                    .Where(x => x.CompanyId == model.Company.CompanyId)
                                                                    .Select(b => b.BrokerId).ToList();

            _companyBrokerService.GetCompanyBrokersID(model.CreateFormSelectedBrokers, existingBrokers,
                                                      out List<int> brokersToRemove, out List<int> brokersToAdd);


            if (brokersToAdd != null)
            {
                foreach (int brokerId in brokersToAdd)
                {
                    CompanyBroker companyBroker = new() { BrokerId = brokerId, CompanyId = model.Company.CompanyId };
                    _unitOfWork.CustomCompanyBrokerRepository.InsertCompanyBroker(companyBroker);
                    _unitOfWork.CustomCompanyBrokerRepository.Save();
                }
            }

            if (brokersToRemove != null)
            {
                foreach (int brokerId in brokersToRemove)
                {
                    _unitOfWork.CustomCompanyBrokerRepository.DeleteCompanyBroker(brokerId, model.Company.CompanyId);
                    _unitOfWork.CustomCompanyBrokerRepository.Save();
                }
            }

            return View("Index", _unitOfWork.CompanyRepository.GetAll());
        }

        [HttpPost]
        public IActionResult SortBy(CompanyBrokersModel model, string companyName)//todo same logic in CompanyBrokers method
        {
            IQueryable<Company> companies = _unitOfWork.CompanyRepository.GetAll();
            int companyId = companies.Where(x => x.CompanyName == companyName).Select(x => x.CompanyId).FirstOrDefault();
            List<int> brokersIds = _unitOfWork.CompanyBrokerRepository.GetAll().Where(x => x.CompanyId == companyId)
                                                                      .Select(b => b.BrokerId).ToList();

            CompanyBrokersModel data = new();
            data.CompanyName = companies.Where(x => x.CompanyId == companyId)
                                                                      .Select(b => b.CompanyName).FirstOrDefault();

            data.Brokers = _unitOfWork.BrokerRepository.GetAll()
                                                       .Where(x => brokersIds.Contains(x.BrokerId));

            data.Brokers = model.FilterSort.SortOrder == "Name" ? data.Brokers.OrderBy(x => x.Name)
                                                                : data.Brokers.OrderBy(x => x.Surname);

            data.CompanyName = companyName;
            return View("CompanyBrokers", data);
        }
    }
}
