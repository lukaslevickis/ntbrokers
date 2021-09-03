using System;
using System.Collections.Generic;
using Dapper;
using NTBrokers.DAL.Entities;
using NTBrokers.Helpers;
using NTBrokers.Models.Apartments;
using NTBrokers.Models.Brokers;

namespace NTBrokers.DAL.Repositories
{
    public class BrokerRepository
    {
        private readonly ApplicationDbContext _context;

        public BrokerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SubmitApartment(int brokerId, int apartmentId)
        {
            string query = $"update dbo.House set BrokerId = {brokerId} WHERE ID = {apartmentId};";

            ConnectionsHelpers.ExecuteQuery(query, _context);
        }

        public void InsertBroker(Broker broker)
        {
            _context.Brokers.Add(broker);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
