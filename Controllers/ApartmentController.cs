using Microsoft.AspNetCore.Mvc;
using NTBrokers.Models.Apartments;
using NTBrokers.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NTBrokers.Controllers
{
    public class ApartmentController : Controller
    {
        private ApartmentService _apartmentService;

        public ApartmentController(ApartmentService apartmentService)
        {
            _apartmentService = apartmentService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_apartmentService.GetAll());
        }

        public IActionResult Create()
        {
            return View(_apartmentService.Create());
        }

        public IActionResult Submit(ApartmentCreateModel model)
        {
            _apartmentService.Submit(model);
            return View("Index", _apartmentService.GetAll());
        }
    }
}
