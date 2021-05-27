using System;
using System.Collections;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using TaNoMenu.Db.Seed;

namespace TaNoMenu.Repositories
{
    public abstract class AbstractRepository<T>
    {
        private string _connectionString;
        protected string ConnectionString => _connectionString;
        public AbstractRepository(IConfiguration configuration)
        {


            _connectionString = configuration["DatabaseSettings:ConnectionString"];
            DatabaseSeed.CreateDb(configuration);

            
        }
        public abstract void Add(T item);
        public abstract void Remove(int id);
        public abstract void Update(T item);
        public abstract T FindById(int id);
        public abstract IEnumerable FindAll();
    }
}