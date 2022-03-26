using System.ComponentModel.DataAnnotations;

namespace FruitShop.V1.Controllers.Buys.Request
{
    public class BuyRequest
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int FruitId { get; set; }

        [Required]
        public int Quantity { get; set; }

    }
}
