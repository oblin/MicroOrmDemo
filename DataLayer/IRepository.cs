using System.Collections.Generic;

namespace DataLayer
{
    public interface IRepository<T>
    {
        T Find(int id);
        IEnumerable<T> GetAll();
        T Add(T contact);
        T Update(T contact);
        void Remove(int id);

        void Save(T contact);
    }
}