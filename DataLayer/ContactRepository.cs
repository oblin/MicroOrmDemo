using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class ContactRepository : IContactRepository
    {
        private readonly IDbConnection _connection;
        public ContactRepository()
        {
            var connectionString = GetConnectionStringFromConfig();
            _connection = new NpgsqlConnection(connectionString);
        }

        public ContactRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public Contact Add(Contact contact)
        {
            var sql = "INSERT INTO contacts (firstName, lastname, email, company, title) VALUES (@FirstName, @LastName, @Email, @Company, @Title) " +
                      "RETURNING id as int";
            var id = _connection.Query<int>(sql, contact).Single();
            contact.Id = id;

            return contact;
        }

        public Contact Find(int id)
        {
            return _connection.Query<Contact>($"SELECT * FROM Contacts WHERE Id = {id}").SingleOrDefault();
        }

        public List<Contact> GetContactsById(params int[] ids)
        {
            return this._connection.Query<Contact>("SELECT * FROM contacts WHERE id = any(@ids)", new { ids }).ToList();
        }

        public List<dynamic> GetDynamicsById(params int[] ids)
        {
            return this._connection.Query("SELECT * FROM contacts WHERE id = any(@ids)", new { ids }).ToList();
        }

        public IEnumerable<Contact> GetAll()
        {
            return this._connection.Query<Contact>("SELECT * FROM CONTACTS");
        }

        public Contact GetFullContact(int id)
        {
            var sql = "SELECT * FROM Contacts WHERE Id = @Id; " +
                      "SELECT * FROM Addresses WHERE @ContactId = @Id";
            using (var multipleResults = _connection.QueryMultiple(sql, new { id }))
            {
                var contact = multipleResults.Read<Contact>().SingleOrDefault();
                var addresses = multipleResults.Read<Address>().ToList();
                if (contact != null && addresses != null)
                    contact.Addresses.AddRange(addresses); 

                return contact; 
            }
        }

        public void Remove(int id)
        {
            this._connection.Execute($"DELETE FROM Contacts WHERE Id = {id}");
        }

        // public void Save(Contact contact)
        // {
        //     // TransactionScope 尚未被 Dotnet Core 實作，但會在下一版本中加入
        //     // using (var scope = new System.Transaction.TransactionScope())
        //     // {
        //     _connection.Open();
        //     using (var scope = _connection.BeginTransaction())
        //     {
        //         if (contact.IsNew)
        //             this.Add(contact);
        //         else
        //             this.Update(contact);

        //         foreach (var address in contact.Addresses.Where(a => !a.IsDeleted))
        //         {
        //             address.ContactId = contact.Id;
        //             if (address.IsNew)
        //                 this.Add(address);
        //             else
        //                 this.Update(address);
        //         }

        //         foreach (var address in contact.Addresses.Where(a => a.IsDeleted))
        //             _connection.Execute("DELETE FROM Addresses WHERE Id = @Id", new { address.Id });
        //     }
        // }

        public void Save(Contact contact)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_id", value: contact.Id, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            parameters.Add("p_firstname", contact.FirstName, dbType: DbType.String);
            parameters.Add("p_lastname", contact.LastName);
            parameters.Add("p_company", contact.Company);
            parameters.Add("p_title", contact.Title);
            parameters.Add("p_email", contact.Email);
            this._connection.Execute("savecontact", parameters, commandType: CommandType.StoredProcedure);
            contact.Id = parameters.Get<int>("p_id");

            foreach (var address in contact.Addresses.Where(a => !a.IsDeleted))
            {
                address.ContactId = contact.Id;
                var addrParameters = new DynamicParameters(new {
                    p_contactid = address.ContactId,
                    p_addresstype = address.AddressType,
                    p_streetaddress = address.StreetAddress,
                    p_city = address.City,
                    p_stateid = address.StateId,
                    p_postalcode = address.PostalCode
                });
                addrParameters.Add("p_id", address.Id, DbType.Int32, ParameterDirection.InputOutput);
                _connection.Execute("saveaddress", addrParameters, commandType: CommandType.StoredProcedure);
                address.Id = addrParameters.Get<int>("p_id");
            }

            foreach (var address in contact.Addresses.Where(a => a.IsDeleted))
                _connection.Execute("DELETE FROM Addresses WHERE Id = @Id", new { address.Id });
        }

        public Address Add(Address address)
        {
            var sql = "INSERT INTO Addresses (ContactId, AddressType, StreetAddress, City, StateId, PostalCode) VALUES (@ContactId, @AddressType, @StreetAddress, @City, @StateId, @PostalCode) " +
                      "RETURNING Id as int";
            var id = _connection.Query<int>(sql, address).Single();
            address.Id = id;
            return address;
        }

        public Address Update(Address address)
        {
            var sql = "UPDATE Addresses SET ContactId = @ContactId, AddressType = @AddressType, StreetAddress = @StreetAddress, City = @City, StateId = @StateId, PostalCode = @PostalCode " +
                      "WHERE Id = @Id";
            _connection.Execute(sql, address);
            return address;
        }

        public Contact Update(Contact contact)
        {
            var sql = "UPDATE Contacts SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Company = @Company, Title = @Title " +
                      "WHERE Id = @Id";
            _connection.Execute(sql, contact);
            return contact;
        }

        private static string GetConnectionStringFromConfig()
        {
            var config = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("config.json")
                              .Build();
            return config["ConnectionStrings:Postgresql"];
        }

        public int BulkInsertContacts(List<Contact> contacts)
        {
            var sql = "INSERT INTO contacts (firstName, lastname, email, company, title) VALUES (@FirstName, @LastName, @Email, @Company, @Title) " +
                      "RETURNING id as int";
            return this._connection.Execute(sql, contacts);
        }
    }
}
