using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using NTBrokers.Models.Brokers;

namespace NTBrokers.Models.Companies
{
    public class CompanyCreateModel
    {
        public List<SelectListItem> CreateFormBrokers { set; get; } = new List<SelectListItem>();
        public string[] CreateFormSelectedBrokers { set; get; }
        public List<BrokerModel> Brokers { get; set; }
        public CompanyModel Company { get; set; }
    }
}
