using System.Threading.Tasks;
using NTBrokers.DAL.Entities;

namespace NTBrokers.DAL.Repositories
{
    public interface ICompanyBrokerRepository: IGenericRepository<CompanyBroker>
    {
        Task DeleteCompanyBrokerAsync(int brokerId, int companyId);
    }
}
