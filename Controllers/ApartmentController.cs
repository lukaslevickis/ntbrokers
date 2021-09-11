using System.Threading.Tasks;
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
        [ActionName("Index")]
        public async Task<IActionResult> IndexAsync()
        {
            return View(await _apartmentService.GetAllAsync());
        }

        [ActionName("Create")]
        public async Task<IActionResult> CreateAsync()
        {
            return View(await _apartmentService.CreateAsync());
        }

        public async Task<IActionResult> SubmitAsync(ApartmentCreateModel model)
        {
            await _apartmentService.SubmitAsync(model);
            return View("Index", await _apartmentService.GetAllAsync());
        }
    }
}
