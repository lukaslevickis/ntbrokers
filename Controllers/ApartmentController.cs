using Microsoft.AspNetCore.Mvc;
using NTBrokers.DAL;
using NTBrokers.Models.Apartments;
using NTBrokers.Models.Companies;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NTBrokers.Controllers
{
    public class ApartmentController : Controller
    {
        private UnitOfWork _unitOfWork;

        public ApartmentController(ApplicationDbContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_unitOfWork.CustomApartmentRepository.GetAll());
        }

        public IActionResult Create()
        {
            CompanyModel company = new();
            var aa = new ApartmentCreateModel
            {
                Apartment = new ApartmentModel(),
                //Companies = _unitOfWork.CompanyRepository.GetAll()
            };

            return View(aa);
        }

        public IActionResult Submit(ApartmentCreateModel model)
        {
            _unitOfWork.CustomApartmentRepository.Create(model.Apartment);
            return View("Index", _unitOfWork.CustomApartmentRepository.GetAll());
        }
    }
}
