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
    public class ApartmentController : Controller
    {
        private readonly ApartmentDBService _apartmentDBService;
        private readonly RealEstateService _realEstateService;

        public ApartmentController(ApartmentDBService apartmentDBService, RealEstateService realEstateService)
        {
            _apartmentDBService = apartmentDBService;
            _realEstateService = realEstateService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            RealEstateModel data = new();
            data.Apartments = _apartmentDBService.Read();
            return View(data);
        }

        public IActionResult Create()
        {
            RealEstateModel data = _realEstateService.GetGeneralDBData();
            return View(data);
        }

        public IActionResult Submit(RealEstateModel model)
        {
            _apartmentDBService.Create(model);
            RealEstateModel data = new();
            data.Apartments = _apartmentDBService.Read();
            return View("Index", data);
        }
    }
}
