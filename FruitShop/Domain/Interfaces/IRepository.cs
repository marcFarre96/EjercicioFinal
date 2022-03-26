using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);

        void Delete(int entityId);

        T Get(int entityId);

        IEnumerable<T> GetAll();
    }
}
