using Microsoft.EntityFrameworkCore;
using NTBrokers.DAL.Entities;

namespace NTBrokers.DAL.Repositories
{
    public class CompanyBrokerRepository: GenericRepository<CompanyBroker>, ICompanyBrokerRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyBrokerRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public virtual void DeleteCompanyBroker(int brokerId, int companyId)
        {
            CompanyBroker entityToDelete = _context.CompanyBrokers.Find(brokerId, companyId);
            Remove(entityToDelete);
        }

        public virtual void Remove(CompanyBroker entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _context.Attach(entityToDelete);
            }

            _context.Remove(entityToDelete);
        }
    }
}
