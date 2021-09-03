using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using NTBrokers.Models.Brokers;

namespace NTBrokers.Models.Companies
{
    public class CompanyCreateModel
    {
        public List<SelectListItem> CreateFormBrokers { set; get; } = new List<SelectListItem>();
        public string[] CreateFormSelectedBrokers { set; get; }
        public IQueryable<BrokerModel> Brokers { get; set; }
        public List<BrokerModel> SelectedBrokers { get; set; }
        public CompanyModel Company { get; set; }
    }
}
