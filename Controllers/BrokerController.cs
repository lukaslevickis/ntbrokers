using Microsoft.AspNetCore.Mvc;
using NTBrokers.DAL.Entities;
using NTBrokers.Models.Brokers;
using NTBrokers.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NTBrokers.Controllers
{
    public class BrokerController : Controller
    {
        private BrokerService _brokerService;

        public BrokerController(BrokerService brokerService)
        {
            _brokerService = brokerService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_brokerService.GetAll());
        }

        public IActionResult Submit(Broker model)
        {
            _brokerService.Insert(model);
            return View("Index", _brokerService.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult BrokerApartments(int brokerId)
        {
            return View("BrokerApartments", _brokerService.GetBrokerApartments(brokerId));
        }

        public IActionResult AddApartment(int brokerId)
        {
            return View("AddApartment", _brokerService.AddApartment(brokerId));
        }

        public IActionResult UpdateApartment(int brokerId, int apartmentId)
        {
            _brokerService.UpdateApartment(brokerId, apartmentId);
            return View("BrokerApartments", _brokerService.GetBrokerApartments(brokerId));
        }

        [HttpPost]
        public IActionResult Filter(BrokerApartmentsModel model, int brokerId, string broker)
        {
            return View("BrokerApartments", _brokerService.Filter(model, brokerId, broker));
        }
    }
}
