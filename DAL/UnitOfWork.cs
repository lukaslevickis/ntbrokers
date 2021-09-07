using NTBrokers.DAL.Entities;
using NTBrokers.DAL.Repositories;

namespace NTBrokers.DAL
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private GenericRepository<Apartment> _apartmentRepository;
        private GenericRepository<Broker> _brokerRepository;
        private GenericRepository<Company> _companyRepository;
        private GenericRepository<CompanyBroker> _companyBrokerRepository;
        private CompanyRepository _customCompanyRepository;
        private ApartmentRepository _customApartmentRepository;
        private BrokerRepository _customBrokerRepository;
        private CompanyBrokerRepository _customCompanyBrokerRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public GenericRepository<Apartment> ApartmentRepository
        {
            get
            {
                if (this._apartmentRepository == null)
                {
                    this._apartmentRepository = new GenericRepository<Apartment>(_context);
                }

                return _apartmentRepository;
            }
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

        public GenericRepository<CompanyBroker> CompanyBrokerRepository
        {
            get
            {
                if (this._companyBrokerRepository == null)
                {
                    this._companyBrokerRepository = new GenericRepository<CompanyBroker>(_context);
                }

                return _companyBrokerRepository;
            }
        }

        public ApartmentRepository CustomApartmentRepository
        {
            get
            {
                if (this._customApartmentRepository == null)
                {
                    this._customApartmentRepository = new ApartmentRepository(_context);
                }

                return _customApartmentRepository;
            }
        }

        public CompanyRepository CustomCompanyRepository
        {
            get
            {
                if (this._customCompanyRepository == null)
                {
                    this._customCompanyRepository = new CompanyRepository(_context);
                }

                return _customCompanyRepository;
            }
        }

        public BrokerRepository CustomBrokerRepository
        {
            get
            {
                if (this._customBrokerRepository == null)
                {
                    this._customBrokerRepository = new BrokerRepository(_context);
                }

                return _customBrokerRepository;
            }
        }

        public CompanyBrokerRepository CustomCompanyBrokerRepository
        {
            get
            {
                if (this._customCompanyBrokerRepository == null)
                {
                    this._customCompanyBrokerRepository = new CompanyBrokerRepository(_context);
                }

                return _customCompanyBrokerRepository;
            }
        }
    }
}
