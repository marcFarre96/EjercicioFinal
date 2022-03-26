using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repository
{
    public class FruitRepository : IRepository<Fruit>
    {
        private readonly FruitStoreDbContext _fruitStoreDbContext;

        public FruitRepository(FruitStoreDbContext fruitStoreDbContext)
        {
            _fruitStoreDbContext = fruitStoreDbContext;
        }

        public Fruit Add(Fruit entity)
        {
            return _fruitStoreDbContext.Fruit.Add(entity).Entity;
        }

        public void Delete(int entityId)
        {
            _fruitStoreDbContext.Fruit.Remove(_fruitStoreDbContext.Fruit.FirstOrDefault(x => x.FruitId == entityId));
        }

        public Fruit Get(int entityId)
        {
            return _fruitStoreDbContext.Fruit.FirstOrDefault(x => x.FruitId == entityId);
        }

        public IEnumerable<Fruit> GetAll()
        {
            return _fruitStoreDbContext.Fruit;
        }
    }
}
