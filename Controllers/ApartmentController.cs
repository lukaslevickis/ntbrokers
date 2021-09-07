using Microsoft.AspNetCore.Mvc;
using NTBrokers.DAL;
using NTBrokers.DAL.Entities;
using NTBrokers.Models.Apartments;

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
            ApartmentCreateModel data = new()
            {
                Apartment = new Apartment(),
                Companies = _unitOfWork.CompanyRepository.GetAll()
            };

            return View(data);
        }

        public IActionResult Submit(ApartmentCreateModel model)
        {
            _unitOfWork.CustomApartmentRepository.InsertApartment(model.Apartment);
            _unitOfWork.CustomBrokerRepository.Save();
            return View("Index", _unitOfWork.CustomApartmentRepository.GetAll());
        }
    }
}
