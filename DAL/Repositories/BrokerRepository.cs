using System;
using System.Collections.Generic;
using Dapper;
using NTBrokers.Helpers;
using NTBrokers.Models.Apartments;

namespace NTBrokers.DAL.Repositories
{
    public class BrokerRepository
    {
        private readonly DapperContext _context;

        public BrokerRepository(DapperContext context)
        {
            _context = context;
        }

        public void SubmitApartment(int brokerId, int apartmentId)
        {
            string query = $"update dbo.House set BrokerId = {brokerId} WHERE ID = {apartmentId};";

            ConnectionsHelpers.ExecuteQuery(query, _context);
        }
    }
}
