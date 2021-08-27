using System;
using NTBrokers.Models.Brokers;
using NTBrokers.Models.Companies;

namespace NTBrokers.DAL
{
    public class UnitOfWork
    {
        private DapperContext _context;
        private GenericRepository<BrokerModel> _brokerRepository;
        private GenericRepository<CompanyModel> _companyRepository;
        private GenericRepository<CompanyCreateModel> _companyCreatewRepository;

        public UnitOfWork(DapperContext context)
        {
            _context = context;
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

        public GenericRepository<CompanyCreateModel> CompanyCreateRepository
        {
            get
            {
                if (this._companyCreatewRepository == null)
                {
                    this._companyCreatewRepository = new GenericRepository<CompanyCreateModel>(_context);
                }

                return _companyCreatewRepository;
            }
        }
    }
}
