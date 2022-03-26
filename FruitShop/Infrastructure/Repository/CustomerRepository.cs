using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repository
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly FruitStoreDbContext _fruitStoreDbContext;

        public CustomerRepository(FruitStoreDbContext fruitStoreDbContext)
        {
            _fruitStoreDbContext = fruitStoreDbContext;
        }

        public Customer Add(Customer entity)
        {
            return _fruitStoreDbContext.Customer.Add(entity).Entity;
        }

        public void Delete(int entityId)
        {
            _fruitStoreDbContext.Customer.Remove(_fruitStoreDbContext.Customer.FirstOrDefault(x => x.CustomerId == entityId));
        }

        public Customer Get(int entityId)
        {
            return _fruitStoreDbContext.Customer.FirstOrDefault(x => x.CustomerId == entityId);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _fruitStoreDbContext.Customer;
        }
    }
}
