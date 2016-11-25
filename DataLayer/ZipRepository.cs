using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;
using System.Linq;

namespace DataLayer
{
    public class ZipRepository : IRepository<Zip>
    {
        private readonly IDbConnection _connection;
        public ZipRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public Zip Add(Zip zip)
        {
            var sql = "INSERT INTO zip (zip_code, zip_name) VALUES (@ZipCode, @ZipName) ";
            var id = _connection.Execute(sql, zip);

            return zip;
        }

        public Zip Find(int id)
        {
            throw new NotSupportedException("必須要使用 FindByKey 傳入字串搜尋");
        }

        public Zip FindByKey(string code)
        {
            return _connection.Query<Zip>($"SELECT * FROM zip WHERE zip_code = @code", new { code = code } )
                    // .Select<dynamic, Zip>(d => new Zip {
                    //     ZipCode = d.zip_code,
                    //     ZipName = d.zip_name})
                    .SingleOrDefault();
        }

        public IEnumerable<Zip> GetAll()
        {
            return this._connection.Query<Zip>("SELECT * FROM zip");
                            // .Select<dynamic, Zip>(d => new Zip {
                            //     ZipCode = d.zip_code,
                            //     ZipName = d.zip_name});
        }

        public void Remove(int id)
        {
            this._connection.Execute($"DELETE FROM Contacts WHERE Id = {id}");
        }

        public void Save(Zip contact)
        {
            throw new NotImplementedException();
        }

        public Zip Update(Zip zip)
        {
            var sql = "UPDATE zip SET zip_code = @ZipCode, zip_name = @ZipName " +
                      "WHERE zip_code = @ZipCode";
            _connection.Execute(sql, zip);
            return zip;
        }

        private static string GetConnectionStringFromConfig()
        {
            var config = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("config.json")
                              .Build();
            return config["ConnectionStrings:Postgresql"];
        }
    }
}
