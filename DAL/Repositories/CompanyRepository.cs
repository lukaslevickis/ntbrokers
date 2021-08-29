using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using NTBrokers.Helpers;
using NTBrokers.Models.Companies;

namespace NTBrokers.DAL.Repositories
{
    public class CompanyRepository
    {
        private readonly DapperContext _context;

        public CompanyRepository(DapperContext context)
        {
            _context = context;
        }

        public void Create(CompanyCreateModel model)
        {
            string query = $"INSERT into dbo.Company (CompanyName, City, Street, Address) values ('{model.Company.CompanyName}', " +
                           $"'{model.Company.City}', '{model.Company.Street}', '{model.Company.Address}');";

            ConnectionsHelpers.ExecuteQuery(query, _context);

            List<CompanyModel> id = GetById();
            int companyId = id.LastOrDefault().Id;

            string query2 = "";
            foreach (string brokerId in model.CreateFormSelectedBrokers)
            {
                query2 += $"INSERT into dbo.CompanyBroker (BrokerId, CompanyId) values ({brokerId}, {companyId});";
            }

            ConnectionsHelpers.ExecuteQuery(query2, _context);
        }

        public List<CompanyModel> GetById()
        {
            var query = "SELECT dbo.Company.ID from dbo.Company";
            using (var connection = _context.CreateConnection())
            {
                var items = connection.Query<CompanyModel>(query);
                return items.ToList();
            }
        }

        public List<int> CompanyBrokersIdsById(int companyId)
        {
            var query = $"SELECT * FROM dbo.CompanyBroker WHERE CompanyId = {companyId}";
            using (var connection = _context.CreateConnection())
            {
                var items = connection.Query<int>(query);
                return items.ToList();
            }
        }
    }
}
