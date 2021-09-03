using System;
using NTBrokers.DAL.Entities;
using NTBrokers.DAL.Repositories;
using NTBrokers.Models;
using NTBrokers.Models.Apartments;
using NTBrokers.Models.Brokers;
using NTBrokers.Models.Companies;

namespace NTBrokers.DAL
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private GenericRepository<ApartmentModel> _apartmentRepository;
        private GenericRepository<Broker> _brokerRepository;
        private GenericRepository<Company> _companyRepository;
        private GenericRepository<CompanyBroker> _companyBrokerRepository;
        private CompanyRepository _customCompanyRepository;
        private ApartmentRepository _customApartmentRepository;
        private BrokerRepository _customBrokerRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public GenericRepository<ApartmentModel> ApartmentRepository
        {
            get
            {
                if (this._apartmentRepository == null)
                {
                    this._apartmentRepository = new GenericRepository<ApartmentModel>(_context);
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
    }
}
