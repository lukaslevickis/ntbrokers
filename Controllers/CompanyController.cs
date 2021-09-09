using Microsoft.AspNetCore.Mvc;
using NTBrokers.Models.Companies;
using NTBrokers.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NTBrokers.Controllers
{
    public class CompanyController : Controller
    {
        private CompanyService _companyService;

        public CompanyController(CompanyService companyService)
        {
            _companyService = companyService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_companyService.GetAll());
        }

        public IActionResult Create()
        {
            return View(_companyService.Create());
        }

        public IActionResult Submit(CompanyCreateModel model)
        {
            _companyService.Insert(model.Company);
            _companyService.InsertCompanyBroker(model);
            return View("Index", _companyService.GetAll());
        }

        public IActionResult CompanyBrokers(int companyId)
        {
            return View(_companyService.GetCompanyBrokers(companyId));
        }

        public IActionResult Edit(int companyId)
        {
            return View(_companyService.Edit(companyId));
        }

        public IActionResult Update(CompanyCreateModel model)
        {
            _companyService.UpdateCompanyBrokers(model);
            return View("Index", _companyService.GetAll());
        }

        [HttpPost]
        public IActionResult SortBy(CompanyBrokersModel model, string companyName)
        {
            return View("CompanyBrokers", _companyService.SortBy(model, companyName));
        }
    }
}
