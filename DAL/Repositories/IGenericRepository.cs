using System.Collections.Generic;

namespace NTBrokers.DAL.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();
        void Insert(TEntity entity);
        TEntity GetByID(object id);
        void Update(TEntity entity);
        void Delete(object id);
    }
}
