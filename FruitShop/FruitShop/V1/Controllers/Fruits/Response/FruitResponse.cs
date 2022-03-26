using Domain.Models;

namespace FruitShop.V1.Controllers.Fruits.Response
{
    public class FruitResponse
    {
        public FruitResponse()
        {
        }

        public FruitResponse(Fruit fruit)
        {
            FruitId = fruit.FruitId;
            FruitTypeId = fruit.FruitTypeId;
        }

        public int FruitId { get; set; }

        public int FruitTypeId { get; set; }
    }
}
