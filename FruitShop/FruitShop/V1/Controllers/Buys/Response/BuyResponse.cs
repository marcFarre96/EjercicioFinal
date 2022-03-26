using Domain.Models;

namespace FruitShop.V1.Controllers.Buys.Response
{
    public class BuyResponse
    {
        public BuyResponse(Buy buy)
        {
            BuyId = buy.BuyId;
            Quantity = buy.Quantity;
        }

        public int BuyId { get; set; }

        public int Quantity { get; set; }
    }
}
