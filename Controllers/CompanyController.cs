using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NTBrokers.Models;
using NTBrokers.Models.Companies;
using NTBrokers.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NTBrokers.Controllers
{
    public class CompanyController : Controller
    {
        private readonly MainService _mainService;

        public CompanyController(CompanyDBService companyDBService, MainService mainService)
        {
            _mainService = mainService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_mainService._companyDBService.GetAll());
        }

        public IActionResult Create()
        {
            CompanyCreateModel data = new CompanyCreateModel()
            {
                Brokers = _mainService._brokerDBService.GetAll(),
                Company = new CompanyModel()
            };

            return View(data);
        }

        public IActionResult Submit(CompanyCreateModel model)
        {
            _mainService._companyDBService.Create(model);
            return View("Index", _mainService._companyDBService.GetAll());
        }

        public IActionResult CompanyBrokers(int companyId)
        {
            return View(_mainService._companyDBService.CompanyBrokers(companyId));
        }
    }
}
