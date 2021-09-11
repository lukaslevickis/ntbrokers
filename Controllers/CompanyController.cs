using System.Threading.Tasks;
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
        [ActionName("Index")]
        public async Task<IActionResult> IndexAsync()
        {
            return View(await _companyService.GetAllAsync());
        }

        [ActionName("Create")]
        public async Task<IActionResult> CreateAsync()
        {
            return View(await _companyService.CreateAsync());
        }

        public async Task<IActionResult> SubmitAsync(CompanyCreateModel model)
        {
            await _companyService.InsertAsync(model.Company);
            await _companyService.InsertCompanyBrokerAsync(model);
            return View("Index", await _companyService.GetAllAsync());
        }

        [ActionName("CompanyBrokers")]
        public async Task<IActionResult> CompanyBrokersAsync(int companyId)
        {
            return View(await _companyService.GetCompanyBrokersAsync(companyId));
        }

        [ActionName("Edit")]
        public async Task<IActionResult> EditAsync(int companyId)
        {
            return View(await _companyService.EditAsync(companyId));
        }

        public async Task<IActionResult> UpdateAsync(CompanyCreateModel model)
        {
            await _companyService.UpdateCompanyBrokersAsync(model);
            return View("Index", await _companyService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> SortByAsync(CompanyBrokersModel model, string companyName)
        {
            return View("CompanyBrokers", await _companyService.SortByAsync(model, companyName));
        }
    }
}
