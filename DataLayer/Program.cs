using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace DataLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            var connectionString = GetConnectionStringFromConfig();
            var connection = new NpgsqlConnection(connectionString);

            var logger = new LoggerFactory()
                             .AddConsole()
                             .CreateLogger<Program>();

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            DisplayAllContacts(connection);
            DisplayAllZipCode(connection, logger);
            DisplayOneZipCode(connection);
            // AddOneZipCode(connection);
            UpdateOneZipCode(connection);

            MultipleResultSetReader(connection);
        }

        private static void MultipleResultSetReader(NpgsqlConnection connection)
        {
            connection.Open();
            var scope = connection.BeginTransaction();
            var command = new NpgsqlCommand("test", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("param_id", 42);

            var sql = new StringBuilder();
            using (var dr = command.ExecuteReader(CommandBehavior.SequentialAccess))
            {
            while(dr.Read()){
                // Console.WriteLine($"FETCH ALL IN \"{dr.GetString(0)}\"");
                sql.AppendLine($"FETCH ALL IN \"{dr.GetString(0)}\";");
            }
            }
            Console.WriteLine(sql.ToString());
            using (var cmd2 = new NpgsqlCommand())
            {
                cmd2.Connection = command.Connection;
                cmd2.Transaction = scope;
                cmd2.CommandText = sql.ToString();
                cmd2.CommandType = CommandType.Text;
                var dr = cmd2.ExecuteReader();
                while(dr.Read())
                    Console.WriteLine($"{dr[0]} - {dr[1]}");
            }
            scope.Commit();
            connection.Close();
        }

        private static void UpdateOneZipCode(NpgsqlConnection connection)
        {
            var repo = new ZipRepository(connection);
            var zip = repo.FindByKey("0003");
            zip.ZipName = "中王路十二巷";
            repo.Update(zip);
        }

        private static void AddOneZipCode(NpgsqlConnection connection)
        {
            var zip = new Zip { ZipCode = "0004", ZipName = "中王路十四巷" };
            var repo = new ZipRepository(connection);
            repo.Add(zip);
        }

        public static void DisplayAllContacts(IDbConnection connection)
        {
            var repo = new ContactRepository(connection);
            var contacts = repo.GetAll();
            Console.WriteLine($"Hello I have {contacts.Count()} users ");
        }

        public static void DisplayAllZipCode(IDbConnection connection, ILogger logger)
        {
            var zipRepo = new ZipRepository(connection);
            var zips = zipRepo.GetAll();
            foreach (var zip in zips)
            {
                // logger.LogInformation($"Logger: code: {zip.ZipCode}, name: {zip.ZipName}");
                Console.WriteLine($"DisplayAllZipCode: code: {zip.ZipCode}, name: {zip.ZipName}");
            }
        }
        private static void DisplayOneZipCode(NpgsqlConnection connection)
        {
            var zipRepo = new ZipRepository(connection);
            var zip = zipRepo.FindByKey("0001");
            Console.WriteLine($"DisplayOneZipCode: code: {zip.ZipCode}, name: {zip.ZipName}");
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
