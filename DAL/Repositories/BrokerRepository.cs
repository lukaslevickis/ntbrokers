using NTBrokers.DAL.Entities;

namespace NTBrokers.DAL.Repositories
{
    public class BrokerRepository
    {
        private readonly ApplicationDbContext _context;

        public BrokerRepository(ApplicationDbContext context)
        {
            _context = context;
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
