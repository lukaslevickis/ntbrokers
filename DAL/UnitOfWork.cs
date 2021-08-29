using System;
using NTBrokers.DAL.Repositories;
using NTBrokers.Models;
using NTBrokers.Models.Apartments;
using NTBrokers.Models.Brokers;
using NTBrokers.Models.Companies;

namespace NTBrokers.DAL
{
    public class UnitOfWork
    {
        private DapperContext _context;
        private GenericRepository<ApartmentModel> _apartmentRepository;
        private GenericRepository<BrokerModel> _brokerRepository;
        private GenericRepository<CompanyModel> _companyRepository;
        private GenericRepository<CompanyBrokerModel> _companyBrokerRepository;
        private CompanyRepository _customCompanyRepository;
        private ApartmentRepository _customApartmentRepository;
        private BrokerRepository _customBrokerRepository;

        public UnitOfWork(DapperContext context)
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

        public GenericRepository<BrokerModel> BrokerRepository
        {
            get
            {
                if (this._brokerRepository == null)
                {
                    this._brokerRepository = new GenericRepository<BrokerModel>(_context);
                }

                return _brokerRepository;
            }
        }

        public GenericRepository<CompanyModel> CompanyRepository
        {
            get
            {
                if (this._companyRepository == null)
                {
                    this._companyRepository = new GenericRepository<CompanyModel>(_context);
                }

                return _companyRepository;
            }
        }

        public GenericRepository<CompanyBrokerModel> CompanyBrokerRepository
        {
            get
            {
                if (this._companyBrokerRepository == null)
                {
                    this._companyBrokerRepository = new GenericRepository<CompanyBrokerModel>(_context);
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
