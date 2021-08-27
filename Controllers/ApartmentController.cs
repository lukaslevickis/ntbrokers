using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NTBrokers.Models;
using NTBrokers.Models.Apartments;
using NTBrokers.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NTBrokers.Controllers
{
    public class ApartmentController : Controller
    {
        private readonly MainService _mainService;

        public ApartmentController(MainService mainService)
        {
            _mainService = mainService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_mainService._apartmentDBService.GetAll());
        }

        public IActionResult Create()
        {
            return View(new ApartmentIndexModel
                        {
                            Apartment = new ApartmentModel(),
                            Companies = _mainService._companyDBService.GetAll()
                        });
        }

        public IActionResult Submit(ApartmentIndexModel model)
        {
            _mainService._apartmentDBService.Create(model);
            return View("Index", _mainService._apartmentDBService.GetAll());
        }
    }
}
