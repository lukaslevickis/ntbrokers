using System.Linq;

namespace NTBrokers.DAL.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        TEntity GetByID(object id);
        void Update(TEntity entity);
        void Delete(object id);
    }
}
