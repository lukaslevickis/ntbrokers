using System.Linq;
using NTBrokers.DAL.Entities;

namespace NTBrokers.DAL.Repositories
{
    public class CompanyBrokerRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyBrokerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void InsertCompanyBroker(CompanyBroker companyBroker)
        {
            _context.CompanyBrokers.Add(companyBroker);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public virtual void DeleteCompanyBroker(int brokerId, int companyId)
        {
            CompanyBroker entityToDelete = _context.CompanyBrokers.Where(x => x.BrokerId == brokerId && x.CompanyId == companyId).FirstOrDefault();
            _context.CompanyBrokers.Remove(entityToDelete);
        }
    }
}
