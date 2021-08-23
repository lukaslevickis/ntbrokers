using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NTBrokers.Models
{
    public class CompanyAdditionalModel
    {
        public List<SelectListItem> CreateFormBrokers { set; get; } = new List<SelectListItem>();
        public string[] CreateFormSelectedBrokers { set; get; }
    }
}
