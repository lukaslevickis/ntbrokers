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

            List<CompanyModel> id = GetIDs();
            int companyId = id.LastOrDefault().Id;

            foreach (string brokerId in model.CreateFormSelectedBrokers)
            {
                string query2 = $"INSERT into dbo.CompanyBroker (BrokerId, CompanyId) values ({brokerId}, {companyId});";
                ConnectionsHelpers.ExecuteQuery(query2, _context);
            }
        }

        public List<CompanyModel> GetIDs()
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

        public void UpdateCompany(CompanyCreateModel model)
        {
            string query = $"UPDATE dbo.Company SET CompanyName = '{model.Company.CompanyName}', City = '{model.Company.City}', " +
                           $"Street = '{model.Company.Street}', Address = '{model.Company.Address}' " +
                           $"WHERE dbo.Company.ID = {model.Company.Id}";

            ConnectionsHelpers.ExecuteQuery(query, _context);
        }

        public void UpdateRemoveCompanyBrokers(CompanyCreateModel model, List<string> existingBrokers)
        {
            List<string> brokersToRemove = null;
            List<string> brokersToAdd = null;
            List<string> selectedBrokers = model?.CreateFormSelectedBrokers?.ToList();
            if (selectedBrokers == null)
            {
                brokersToRemove = existingBrokers;
            }
            else
            {
                brokersToRemove = existingBrokers.Where(x => !selectedBrokers.Contains(x))?.ToList();
                brokersToAdd = selectedBrokers.Where(x => !existingBrokers.Contains(x)).ToList();
            }

            if (brokersToAdd != null)
            {
                foreach (string brokerId in brokersToAdd)//todo dublication in create method
                {
                    string query = $"INSERT into dbo.CompanyBroker (BrokerId, CompanyId) values ({brokerId}, {model.Company.Id});";
                    ConnectionsHelpers.ExecuteQuery(query, _context);
                }
            }

            if (brokersToRemove != null)
            {
                foreach (string brokerId in brokersToRemove)
                {
                    string query = $"DELETE FROM dbo.CompanyBroker WHERE BrokerId = {brokerId} AND CompanyId = {model.Company.Id}";
                    ConnectionsHelpers.ExecuteQuery(query, _context);
                }
            }
        }
    }
}
