using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NTBrokers.Models;
using NTBrokers.Models.Brokers;
using NTBrokers.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NTBrokers.Controllers
{
    public class BrokerController : Controller
    {
        private readonly MainService _mainService;

        public BrokerController(MainService mainService)
        {
            _mainService = mainService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<BrokerModel> data = _mainService._brokerDBService.GetAll();
            return View(data);
        }

        public IActionResult Submit(BrokerModel model)
        {
            _mainService._brokerDBService.Create(model);
            List<BrokerModel> data = _mainService._brokerDBService.GetAll();
            return View("Index", data);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult BrokerApartments(int brokerId)
        {
            BrokerApartmentModel data = new()
            {
                Broker = _mainService._brokerDBService.GetAll().Where(x => x.Id == brokerId).FirstOrDefault(),
                Apartments = _mainService._brokerDBService.BrokerApartments(brokerId)
            };

            return View("BrokerApartments", data);
        }

        public IActionResult AddApartment(int brokerId)
        {
            BrokerApartmentModel data = new BrokerApartmentModel()
            {
                BrokerId = brokerId,
                Apartments = _mainService._brokerDBService.AddApartment(brokerId)
            };

            return View("AddApartment", data);
        }

        public IActionResult SubmitApartment(int brokerId, int apartmentId)
        {
            _mainService._brokerDBService.SubmitApartment(brokerId, apartmentId);
            BrokerApartmentModel data = new()
            {
                Broker = _mainService._brokerDBService.GetAll().Where(x => x.Id == brokerId).FirstOrDefault(),
                Apartments = _mainService._brokerDBService.BrokerApartments(brokerId)
            };

            return View("BrokerApartments", data);
        }
    }
}
