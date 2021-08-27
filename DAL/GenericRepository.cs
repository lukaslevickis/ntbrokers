using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace NTBrokers.DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DapperContext _context;

        public GenericRepository(DapperContext context)
        {
            _context = context;
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
                rowValues.Add("@" + prop.GetValue(model).ToString());
            }

            var query = $"INSERT INTO {properties.Last().GetValue(model)} ({string.Join(", ", colNames).Trim()}) " +
                        $"VALUES ({string.Join(", ", rowValues).Trim()})";

            using (var connection = _context.CreateConnection())
            {
                connection.Execute(query);
            }
        }

        public List<T> GetAll(string tableName)
        {
            var query = $"SELECT * FROM dbo.{tableName}";
            using (var connection = _context.CreateConnection())
            {
                var companies = connection.Query<T>(query);
                return companies.ToList();
            }
        }
    }
}
