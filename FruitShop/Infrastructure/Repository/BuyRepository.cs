using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repository
{
    public class BuyRepository : IRepository<Buy>
    {
        private readonly FruitStoreDbContext _fruitStoreDbContext;

        public BuyRepository(FruitStoreDbContext fruitStoreDbContext)
        {
            _fruitStoreDbContext = fruitStoreDbContext;
        }

        public Buy Add(Buy entity)
        {
            return _fruitStoreDbContext.Buy.Add(entity).Entity;
        }

        public void Delete(int entityId)
        {
            _fruitStoreDbContext.Buy.Remove(_fruitStoreDbContext.Buy.FirstOrDefault(x => x.BuyId == entityId));
        }

        public Buy Get(int entityId)
        {
            return _fruitStoreDbContext.Buy.FirstOrDefault(x => x.BuyId == entityId);
        }

        public IEnumerable<Buy> GetAll()
        {
            return _fruitStoreDbContext.Buy;
        }
    }
}
