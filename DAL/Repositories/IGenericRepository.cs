using System;
using System.Collections.Generic;
using System.Linq;

namespace NTBrokers.DAL.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        void Create(T model);
        List<T> GetByID(string tableName, string col, int id);
        List<T> SortBy(string col);
    }
}
