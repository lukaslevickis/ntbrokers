using System;
using NTBrokers.DAL;

namespace NTBrokers.Repositories
{
    public class BrokerRepository: IBrokerRepository
    {
        private readonly DapperContext _context;

        public BrokerRepository(DapperContext context)
        {
            _context = context;
        }
    }
}
