using NTBrokers.DAL.Entities;

namespace NTBrokers.DAL.Repositories
{
    public interface ICompanyBrokerRepository: IGenericRepository<CompanyBroker>
    {
        void DeleteCompanyBroker(int brokerId, int companyId);
    }
}
