using System.Threading.Tasks;
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
        [ActionName("Index")]
        public async Task<IActionResult> IndexAsync()
        {
            return View(await _brokerService.GetAllAsync());
        }

        public async Task<IActionResult> SubmitAsync(Broker model)
        {
            await _brokerService.InsertAsync(model);
            return View("Index", await _brokerService.GetAllAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [ActionName("BrokerApartments")]
        public async Task<IActionResult> BrokerApartmentsAsync(int brokerId)
        {
            return View("BrokerApartments", await _brokerService.GetBrokerApartmentsAsync(brokerId));
        }

        [ActionName("AddApartment")]
        public async Task<IActionResult> AddApartmentAsync(int brokerId)
        {
            return View("AddApartment", await _brokerService.AddApartmentAsync(brokerId));
        }

        public async Task<IActionResult> UpdateApartmentAsync(int brokerId, int apartmentId)
        {
            await _brokerService.UpdateApartmentAsync(brokerId, apartmentId);
            return View("BrokerApartments", await _brokerService.GetBrokerApartmentsAsync(brokerId));
        }

        [HttpPost]
        public async Task<IActionResult> FilterAsync(BrokerApartmentsModel model, int brokerId, string broker)
        {
            return View("BrokerApartments", await _brokerService.FilterAsync(model, brokerId, broker));
        }
    }
}
