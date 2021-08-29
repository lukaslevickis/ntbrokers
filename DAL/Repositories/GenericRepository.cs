using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using NTBrokers.Helpers;

namespace NTBrokers.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DapperContext _context;

        public GenericRepository(DapperContext context)
        {
            _context = context;
        }

        //public abstract void Create2(T model);

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
                rowValues.Add("@" + prop.GetValue(model).ToString());
            }

            var query = $"INSERT INTO {properties.Last().GetValue(model)} ({string.Join(", ", colNames).Trim()}) " +
                        $"VALUES ({string.Join(", ", rowValues).Trim()})";

            ConnectionsHelpers.ExecuteQuery(query, _context);
        }

        public List<T> GetAll(string tableName) //todo pass query to this method?
        {
            var query = $"SELECT * FROM dbo.{tableName}";
            using (var connection = _context.CreateConnection())
            {
                var items = connection.Query<T>(query);
                return items.ToList();
            }
        }

        public List<T> GetByID(string col, int id)//todo parameters and table name
        {
            var query = $"SELECT * FROM dbo.CompanyBroker WHERE {col} = {id}";
            using (var connection = _context.CreateConnection())
            {
                var items = connection.Query<T>(query);
                return items.ToList();
            }
        }
    }
}
