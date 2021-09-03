using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.EntityFrameworkCore;
using NTBrokers.Helpers;

namespace NTBrokers.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            this.dbSet = context.Set<T>();
        }

        public void Create(T model)
        {
            List<string> colNames = new();
            List<string> rowValues = new();

            Type t = model.GetType();
            var properties = t.GetProperties();
            foreach (var prop in properties.Skip(1))
            {
                if (prop.Name == "TableName")
                {
                    continue;
                }

                colNames.Add(prop.Name);
                rowValues.Add("'" + prop.GetValue(model).ToString() + "'");
            }

            var query = $"INSERT INTO {properties.Last().GetValue(model)} ({string.Join(", ", colNames).Trim()}) " +
                        $"VALUES ({string.Join(", ", rowValues).Trim()})";

            ConnectionsHelpers.ExecuteQuery(query, _context);
        }

        public IQueryable<T> GetAll()
        {
            return dbSet;
        }

        public List<T> GetByID(string tableName, string col, int id)//todo parameters
        {
            var query = $"SELECT * FROM dbo.{tableName} WHERE {col} = {id}";
            //using (var connection = _context.CreateConnection())
            //{
            //    var items = connection.Query<T>(query);
            //    return items.ToList();
            //}

            throw new NotImplementedException();
        }

        public List<T> SortBy(string col)
        {
            var query = $"SELECT * from dbo.Broker order by {col}";
            //using (var connection = _context.CreateConnection())
            //{
            //    var items = connection.Query<T>(query);
            //    return items.ToList();
            //}
            throw new NotImplementedException();
        }
    }
}
