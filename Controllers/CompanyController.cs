using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NTBrokers.Models;
using NTBrokers.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NTBrokers.Controllers
{
    public class CompanyController : Controller
    {
        private readonly RealEstateService _realEstateService;
        private readonly CompanyDBService _companyDBService;

        public CompanyController(CompanyDBService companyDBService, RealEstateService realEstateService)
        {
            _companyDBService = companyDBService;
            _realEstateService = realEstateService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            RealEstateModel data = new();
            data.Companies = _companyDBService.Read();
            return View(data);
        }

        public IActionResult Create()
        {
            RealEstateModel data = _realEstateService.GetGeneralDBData();
            return View(data);
        }

        public IActionResult Submit(RealEstateModel model)
        {
            _companyDBService.Create(model);
            RealEstateModel data = new();
            data.Companies = _companyDBService.Read();
            return View("Index", data);
        }
    }
}
