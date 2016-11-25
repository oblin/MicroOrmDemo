using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using FluentAssertions;
using Xunit;

namespace Tests
{
    public class DapperSpecificTest
    {
        [Fact]
        public void Bulk_insert_should_insert_3_rows()
        {
        //Given
            var respository = new ContactRepository();
            var contacts= new List<Contact>
            {
                new Contact { FirstName = "Charles", LastName = "Barkley" },
                new Contact { FirstName = "Scotte", LastName = "Pippen" },
                new Contact { FirstName = "Tim", LastName = "Duncan" },
                new Contact { FirstName = "Patrick", LastName = "Ewing" }
            };
        //When
            var rowAffected = respository.BulkInsertContacts(contacts);
        
        //Then
            rowAffected.Should().Be(4);
        }   

        [Fact]
        public void List_support_should_produce_correct_results()
        {
        //Given
            var repository = new ContactRepository();
        //When
            var contacts = repository.GetContactsById(50, 51, 52, 53, 54);
        //Then
            contacts.Count.Should().Be(5);
        }
    }
}