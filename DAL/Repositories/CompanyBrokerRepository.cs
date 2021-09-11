using System.Threading.Tasks;
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

        public virtual async Task DeleteCompanyBrokerAsync(int brokerId, int companyId)
        {
            CompanyBroker entityToDelete = await _context.CompanyBrokers.FindAsync(brokerId, companyId);
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
