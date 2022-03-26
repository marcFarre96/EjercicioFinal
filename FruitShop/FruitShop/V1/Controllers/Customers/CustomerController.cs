using FruitShop.V1.Controllers.Customers.Request;
using FruitShop.V1.Controllers.Customers.Response;
using FruitShop.V1.Controllers.Customers.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FruitShop.V1.Controllers.Customers
{
    [Route("api/v1/customers")]
    [ApiController]
    public class CustomerController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<CustomerResponse> Submit([FromBody] CustomerRequest customerRequest)
        {
            var result = _customerService.Add(customerRequest);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CustomerResponse>> GetAll()
        {
            var result = _customerService.GetAll();
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpGet("{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CustomerResponse> GetCustomer(int customerId)
        {
            var result = _customerService.GetCustomer(customerId);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpDelete("{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CustomerResponse> Remove(int customerId)
        {
            var result = _customerService.Remove(customerId);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
