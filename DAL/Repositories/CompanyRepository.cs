using NTBrokers.DAL.Entities;

namespace NTBrokers.DAL.Repositories
{
    public class CompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void InsertCompany(Company company)
        {
            _context.Companies.Add(company);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
