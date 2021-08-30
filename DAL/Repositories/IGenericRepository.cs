using System;
using System.Collections.Generic;

namespace NTBrokers.DAL.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll(string tableName);
        void Create(T model);
        List<T> GetByID(string tableName, string col, int id);
        List<T> SortBy(string col);
    }
}
