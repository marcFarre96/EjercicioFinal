using FruitShop.V1.Controllers.Buys.Request;
using FruitShop.V1.Controllers.Buys.Response;
using System.Collections.Generic;

namespace FruitShop.V1.Controllers.Buys.Service.Interfaces
{
    public interface IBuyService
    {
        BuyResponse Add(BuyRequest buyRequest);

        IEnumerable<BuyResponse> GetAll();

        BuyResponse GetFruit(int articleId);

        bool Remove(int articleId);
    }
}
