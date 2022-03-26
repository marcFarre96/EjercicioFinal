using System.ComponentModel.DataAnnotations;

namespace FruitShop.V1.Controllers.Customers.Request
{
    public class CustomerRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
