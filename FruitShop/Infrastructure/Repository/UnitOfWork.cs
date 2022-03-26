using Domain.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FruitStoreDbContext _fruitStoreDbContext;

        public UnitOfWork(FruitStoreDbContext fruitStoreDbContext)
        {
            _fruitStoreDbContext = fruitStoreDbContext;
        }

        public int SaveChanges()
        {
            return _fruitStoreDbContext.SaveChanges();
        }
    }
}
