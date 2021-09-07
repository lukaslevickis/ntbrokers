using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NTBrokers.DAL;
using NTBrokers.DAL.Entities;
using NTBrokers.Models.Brokers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NTBrokers.Controllers
{
    public class BrokerController : Controller
    {
        private UnitOfWork _unitOfWork;

        public BrokerController(ApplicationDbContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_unitOfWork.BrokerRepository.GetAll());
        }

        public IActionResult Submit(Broker model)
        {
            _unitOfWork.CustomBrokerRepository.InsertBroker(model);
            _unitOfWork.CustomBrokerRepository.Save();
            IQueryable<Broker> data = _unitOfWork.BrokerRepository.GetAll();

            return View("Index", data);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult BrokerApartments(int brokerId)
        {
            IQueryable<Apartment> apartments = _unitOfWork.ApartmentRepository.GetAll().Where(x => x.BrokerId == brokerId);
            BrokerApartmentsModel data = new()
            {
                Broker = _unitOfWork.BrokerRepository.GetAll().Where(x => x.BrokerId == brokerId).FirstOrDefault(),
                Apartments = apartments,
                SelectApartments = apartments.Select(x => x.City).Distinct().ToList()
            };

            return View("BrokerApartments", data);
        }

        public IActionResult AddApartment(int brokerId)
        {
            List<int> companiesIds = _unitOfWork.CompanyBrokerRepository.GetAll()
                                                                      .Select(x => x.CompanyId).ToList();

            List<string> brokerCompaniesNames = _unitOfWork.CompanyRepository
                                                           .GetAll()
                                                           .Where(company => companiesIds.Contains(company.CompanyId)).ToList()
                                                           .Select(x => x.CompanyName).ToList();

            AddApartmentModel data = new ()
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
            IQueryable<Apartment> apartments = _unitOfWork.ApartmentRepository.GetAll();
            Apartment apartment = apartments.Where(x => x.Id == apartmentId).FirstOrDefault();
            apartment.BrokerId = brokerId;
            _unitOfWork.ApartmentRepository.Update(apartment);
            _unitOfWork.ApartmentRepository.Save();
            BrokerApartmentsModel data = new()
            {
                Broker = _unitOfWork.BrokerRepository.GetAll().Where(x => x.BrokerId == brokerId).FirstOrDefault(),
                Apartments = _unitOfWork.ApartmentRepository.GetAll().Where(x => x.BrokerId == brokerId),
                SelectApartments = apartments.Select(x => x.City).Distinct().ToList()
            };

            return View("BrokerApartments", data);
        }

        [HttpPost]
        public IActionResult Filter(BrokerApartmentsModel model, int brokerId, string broker)
        {
            IQueryable<Apartment> apartments = _unitOfWork.ApartmentRepository.GetAll().Where(x => x.BrokerId == brokerId);
            BrokerApartmentsModel data = new()
            {
                Broker = _unitOfWork.BrokerRepository.GetAll().Where(x => x.BrokerId == brokerId).FirstOrDefault(),
                Apartments = apartments.Where(x => x.City.Contains(model.FilterSort.FilterCity)),
                SelectApartments = apartments.Select(x => x.City).Distinct().ToList(),
                BrokerName = broker
            };

            return View("BrokerApartments", data);
        }
    }
}
