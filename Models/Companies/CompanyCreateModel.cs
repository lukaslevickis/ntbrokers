using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using NTBrokers.DAL.Entities;

namespace NTBrokers.Models.Companies
{
    public class CompanyCreateModel
    {
        public List<SelectListItem> CreateFormBrokers { set; get; } = new List<SelectListItem>();
        public int[] CreateFormSelectedBrokers { set; get; }
        public List<Broker> Brokers { get; set; }
        public List<Broker> SelectedBrokers { get; set; }
        public Company Company { get; set; }
    }
}
