using System.Collections.Generic;

namespace DataLayer
{
    public class Contact
    {
        public Contact ()
        {
            this.Addresses = new List<Address>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }

        public List<Address> Addresses { get; private set; }

        public bool IsNew => this.Id == default(int);
    }
}
