using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using FluentAssertions;
using Xunit;

namespace Tests
{
    // see example explanation on xUnit.net website:
    // https://xunit.github.io/docs/getting-started-dotnet-core.html
    public class ContactRepositoryTest
    {
        [Fact]
        public void Get_all_should_return_6_results()
        {
            var respository = CreateRepository();
            var contacts = respository.GetAll();

            contacts.Should().NotBeNull();
            contacts.Count().Should().Be(6);
        }

        static int id = 42; // Should changed after Insert!

        [Fact]
        public void Insert_should_assign_identity_to_new_entity()
        {
            var repository = CreateRepository();
            var contact = new Contact 
            {
                FirstName = "Joe",
                LastName = "Blow",
                Email = "joe.blow@gmail.com",
                Company = "Microsoft",
                Title = "Developer"
            };
            var address = new Address
            {
                AddressType = "Home",
                StreetAddress = "1234 Main Street",
                City = "Baltimore",
                StateId = 1,
                PostalCode = "22222"
            };
            contact.Addresses.Add(address);
            // repository.Add(contact);
            repository.Save(contact);

            contact.Id.Should().NotBe(0, "新增時候要自動寫回資料庫指定的 Id");
            id = contact.Id;
            Console.WriteLine($"Insert Id is {id}");
        }

        [Fact]
        public void Find_should_retrieve_existing_entity()
        {
            var repository = CreateRepository();
            // var contact = repository.Find(id);
            var contact = repository.GetFullContact(id);

            contact.Should().NotBeNull();
            contact.Id.Should().Be(id);
            contact.FirstName.Should().Be("Joe");
            contact.Company.Should().Be("Microsoft");

            contact.Addresses.Count.Should().Be(1);
            contact.Addresses.First().StreetAddress.Should().Be("1234 Main Street");
        }

        [Fact]
        public void Modify_should_update_existing_entity()
        {
        //Given
            var repository = CreateRepository();
        //When
            // var contact = repository.Find(id);
            var contact = repository.GetFullContact(id);

            contact.FirstName = "Bob";
            contact.Addresses[0].StreetAddress = "456 Main Street";
            repository.Save(contact);
        
            var repository2 = CreateRepository();
            var modifiedContact = repository2.GetFullContact(id);

        //Then
            modifiedContact.FirstName.Should().Be("Bob");
            modifiedContact.Addresses.First().StreetAddress.Should().Be("456 Main Street");
        }

        [Fact]
        public void Delete_should_remove_entity()
        {
        //Given
            var repository = CreateRepository();
        //When
            repository.Remove(id);

            var repository2 = CreateRepository();
            var deletedEntity = repository.Find(id);
        //Then
            deletedEntity.Should().BeNull();
        }

        private IContactRepository CreateRepository()
        {
            return new ContactRepository();
        }
    }
}
