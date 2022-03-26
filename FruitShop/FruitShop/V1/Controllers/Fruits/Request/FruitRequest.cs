using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FruitShop.V1.Controllers.Fruits.Request
{
    public class FruitRequest
    {
        [Required]
        public FruitTypeEnum FruitType { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
