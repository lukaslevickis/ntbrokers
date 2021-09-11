using System;
using System.Threading.Tasks;
using NTBrokers.DAL.Entities;
using NTBrokers.DAL.Repositories;

namespace NTBrokers.DAL
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private GenericRepository<Broker> _brokerRepository;
        private GenericRepository<Company> _companyRepository;
        private CompanyBrokerRepository _companyBrokerRepository;
        private ApartmentRepository _apartmentRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public GenericRepository<Broker> BrokerRepository
        {
            get
            {
                if (this._brokerRepository == null)
                {
                    this._brokerRepository = new GenericRepository<Broker>(_context);
                }

                return _brokerRepository;
            }
        }

        public GenericRepository<Company> CompanyRepository
        {
            get
            {
                if (this._companyRepository == null)
                {
                    this._companyRepository = new GenericRepository<Company>(_context);
                }

                return _companyRepository;
            }
        }

        public CompanyBrokerRepository CompanyBrokerRepository
        {
            get
            {
                if (this._companyBrokerRepository == null)
                {
                    this._companyBrokerRepository = new CompanyBrokerRepository(_context);
                }

                return _companyBrokerRepository;
            }
        }

        public ApartmentRepository ApartmentRepository
        {
            get
            {
                if (this._apartmentRepository == null)
                {
                    this._apartmentRepository = new ApartmentRepository(_context);
                }

                return _apartmentRepository;
            }
        }
    }
}
