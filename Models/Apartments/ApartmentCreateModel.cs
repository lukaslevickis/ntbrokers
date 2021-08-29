using System;
using System.Collections.Generic;
using NTBrokers.Models.Companies;

namespace NTBrokers.Models.Apartments
{
    public class ApartmentCreateModel
    {
        public ApartmentModel Apartment { get; set; } = new ApartmentModel();
        public List<CompanyModel> Companies { get; set; } = new List<CompanyModel>();
    }
}
