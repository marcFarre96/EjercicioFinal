using FruitShop.V1.Controllers.Customers.Request;
using FruitShop.V1.Controllers.Customers.Response;
using System.Collections.Generic;

namespace FruitShop.V1.Controllers.Customers.Service.Interfaces
{
    public interface ICustomerService
    {
        CustomerResponse Add(CustomerRequest customerRequest);

        IEnumerable<CustomerResponse> GetAll();

        CustomerResponse GetCustomer(int customerId);

        bool Remove(int customerId);
    }
}
