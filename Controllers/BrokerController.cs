using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NTBrokers.Models;
using NTBrokers.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NTBrokers.Controllers
{
    public class BrokerController : Controller
    {
        private readonly BrokerDBService _brokerDBService;

        public BrokerController(BrokerDBService brokerDBService)
        {
            _brokerDBService = brokerDBService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<BrokerModel> data = _brokerDBService.Read();
            return View(data);
        }

        public IActionResult Submit(BrokerModel model)
        {
            _brokerDBService.Create(model);
            List<BrokerModel> data = _brokerDBService.Read();
            return View("Index", data);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult BrokerApartments(int brokerId)
        {
            RealEstateModel data = new();
            data.Broker = _brokerDBService.Read().Where(x => x.Id == brokerId).FirstOrDefault();
            data.Apartments = _brokerDBService.BrokerApartments(brokerId);
            return View("BrokerApartments", data);
        }

        public IActionResult AddApartment(int brokerId)
        {
            RealEstateModel data = new();
            data.Broker.Id = brokerId;
            data.Apartments = _brokerDBService.AddApartment(brokerId);
            return View("AddApartment", data);
        }

        public IActionResult SubmitApartment(int brokerId, int apartmentId)
        {
            _brokerDBService.SubmitApartment(brokerId, apartmentId);
            RealEstateModel data = new();
            data.Broker = _brokerDBService.Read().Where(x => x.Id == brokerId).FirstOrDefault();
            data.Apartments = _brokerDBService.BrokerApartments(brokerId);
            return View("BrokerApartments", data);
        }
    }
}
