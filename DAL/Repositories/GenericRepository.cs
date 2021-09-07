using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace NTBrokers.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return dbSet;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }
    }
}
