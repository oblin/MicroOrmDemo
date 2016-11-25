
namespace DataLayer
{
    public interface IContactRepository : IRepository<Contact>
    {
        Contact GetFullContact(int id);
    }
}
