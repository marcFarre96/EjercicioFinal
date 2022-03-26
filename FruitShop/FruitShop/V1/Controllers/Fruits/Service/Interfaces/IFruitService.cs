using FruitShop.V1.Controllers.Fruits.Request;
using FruitShop.V1.Controllers.Fruits.Response;
using System.Collections.Generic;

namespace FruitShop.V1.Controllers.Fruits.Service.Interfaces
{
    public interface IFruitService
    {
        FruitResponse Add(FruitRequest fruitRequest);

        IEnumerable<FruitResponse> GetAll();

        FruitResponse GetFruit(int fruitId);

        bool Remove(int fruitId);
    }
}
