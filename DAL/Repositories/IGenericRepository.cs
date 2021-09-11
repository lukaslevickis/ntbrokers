using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTBrokers.DAL.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();
        Task InsertAsync(TEntity entity);
        Task<TEntity> GetByIDAsync(object id);
        void Update(TEntity entity);
        Task DeleteAsync(object id);
    }
}
