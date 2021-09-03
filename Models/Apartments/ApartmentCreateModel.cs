using System;
using System.Collections.Generic;
using System.Linq;
using NTBrokers.Models.Companies;

namespace NTBrokers.Models.Apartments
{
    public class ApartmentCreateModel
    {
        public ApartmentModel Apartment { get; set; } = new ApartmentModel();
        public IQueryable<CompanyModel> Companies { get; set; }// = new IQueryable<CompanyModel>();
    }
}
