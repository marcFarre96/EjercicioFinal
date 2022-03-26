using Domain.Models;

namespace FruitShop.V1.Controllers.Customers.Response
{
    public class CustomerResponse
    {
        public CustomerResponse(Customer customer)
        {
            CustomerId = customer.CustomerId;
            Name = customer.Name;
        }
        public int CustomerId { get; set; }

        public string Name { get; set; }
    }
}
