using System;
using System.Collections.Generic;

namespace NTBrokers.DAL
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll(string tableName);
        void Create(T model);
    }
}
