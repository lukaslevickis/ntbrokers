using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NTBrokers.DAL;
using NTBrokers.Models;
using NTBrokers.Models.Brokers;
using NTBrokers.Models.Companies;
using NTBrokers.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NTBrokers.Controllers
{
    public class CompanyController : Controller
    {
        private UnitOfWork _unitOfWork;

        public CompanyController(DapperContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            CompanyModel company = new();
            return View(_unitOfWork.CompanyRepository.GetAll(company.TableName));
        }

        public IActionResult Create()
        {
            BrokerModel broker = new();
            CompanyCreateModel data = new CompanyCreateModel()
            {
                Brokers = _unitOfWork.BrokerRepository.GetAll(broker.TableName),
                Company = new CompanyModel()
            };

            return View(data);
        }

        public IActionResult Submit(CompanyCreateModel model)
        {
            BrokerModel broker = new();
            _unitOfWork.CompanyCreateRepository.Create(model);
            //_unitOfWork.CompanyRepository.Create(model.CreateFormSelectedBrokers);
            return View("Index", _unitOfWork.CompanyRepository.GetAll(broker.TableName));
        }

        //public IActionResult CompanyBrokers(int companyId)
        //{
        //    return View(_mainService._companyDBService.CompanyBrokers(companyId));
        //}
    }
}
