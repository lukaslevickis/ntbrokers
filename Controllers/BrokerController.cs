using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NTBrokers.DAL;
using NTBrokers.Models;
using NTBrokers.Models.Apartments;
using NTBrokers.Models.Brokers;
using NTBrokers.Models.Companies;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NTBrokers.Controllers
{
    public class BrokerController : Controller
    {
        private UnitOfWork _unitOfWork;

        public BrokerController(DapperContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            BrokerModel broker = new();
            List<BrokerModel> data = _unitOfWork.BrokerRepository.GetAll(broker.TableName);

            return View(data);
        }

        public IActionResult Submit(BrokerModel model)
        {
            _unitOfWork.BrokerRepository.Create(model);
            BrokerModel broker = new();
            List<BrokerModel> data = _unitOfWork.BrokerRepository.GetAll(broker.TableName);

            return View("Index", data);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult BrokerApartments(int brokerId)
        {
            BrokerModel broker = new();
            List<ApartmentModel> apartments = _unitOfWork.CustomApartmentRepository.GetAll().Where(x => x.BrokerId == brokerId).ToList();
            BrokerApartmentsModel data = new()
            {
                Broker = _unitOfWork.BrokerRepository.GetAll(broker.TableName).Where(x => x.Id == brokerId).FirstOrDefault(),
                Apartments = apartments,
                SelectApartments = apartments.Select(x => x.City).Distinct().ToList()
            };

            return View("BrokerApartments", data);
        }

        public IActionResult AddApartment(int brokerId)
        {
            CompanyModel companyModel = new();
            List<int> companiesIds = _unitOfWork.CompanyBrokerRepository.GetByID("CompanyBroker", "BrokerId", brokerId)
                                                                      .Select(x => x.CompanyId).ToList();

            List<string> brokerCompaniesNames = _unitOfWork.CompanyRepository
                                                           .GetAll(companyModel.TableName)
                                                           .Where(company => companiesIds.Contains(company.Id)).ToList()
                                                           .Select(x => x.CompanyName).ToList();

            BrokerApartmentsModel data = new BrokerApartmentsModel()
            {
                BrokerId = brokerId,
                Apartments = _unitOfWork.CustomApartmentRepository
                                        .GetAll().Where(a => string.IsNullOrEmpty(a.BrokerId.ToString())).ToList()
                                        .Where(b => brokerCompaniesNames.Contains(b.CompanyName)).ToList()
            };

            return View("AddApartment", data);
        }

        public IActionResult SubmitApartment(int brokerId, int apartmentId)
        {
            BrokerModel broker = new();
            _unitOfWork.CustomBrokerRepository.SubmitApartment(brokerId, apartmentId);
            BrokerApartmentsModel data = new()
            {
                Broker = _unitOfWork.BrokerRepository.GetAll(broker.TableName).Where(x => x.Id == brokerId).FirstOrDefault(),
                Apartments = _unitOfWork.CustomApartmentRepository.GetAll().Where(x => x.BrokerId == brokerId).ToList()
            };

            return View("BrokerApartments", data);
        }

        [HttpPost]
        public IActionResult Filter(BrokerApartmentsModel model, int brokerId, string broker)
        {
            BrokerModel brokerModel = new();
            List<ApartmentModel> apartments = _unitOfWork.CustomApartmentRepository.GetAll().Where(x => x.BrokerId == brokerId).ToList();
            BrokerApartmentsModel data = new()
            {
                Broker = _unitOfWork.BrokerRepository.GetAll(brokerModel.TableName).Where(x => x.Id == brokerId).FirstOrDefault(),
                Apartments = apartments.Where(x => x.City.Contains(model.FilterSort.FilterCity)).ToList(),
                SelectApartments = apartments.Select(x => x.City).Distinct().ToList(),
                BrokerName = broker
            };

            return View("BrokerApartments", data);
        }
    }
}
