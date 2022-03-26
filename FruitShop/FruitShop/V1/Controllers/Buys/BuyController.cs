using FruitShop.V1.Controllers.Buys.Request;
using FruitShop.V1.Controllers.Buys.Response;
using FruitShop.V1.Controllers.Buys.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FruitShop.V1.Controllers.Buys
{
    [Route("api/v1/buys")]
    [ApiController]
    public class BuyController
    {
        private readonly IBuyService _buyService;

        public BuyController(IBuyService buyService)
        {
            _buyService = buyService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<BuyResponse> Submit([FromBody] BuyRequest buyRequest)
        {
            var result = _buyService.Add(buyRequest);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<BuyResponse>> GetAll()
        {
            var result = _buyService.GetAll();
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpGet("{buyId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<BuyResponse> GetPurchase(int fruitId)
        {
            var result = _buyService.GetFruit(fruitId);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpDelete("{buyId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<BuyResponse> Remove(int fruitId)
        {
            var result = _buyService.Remove(fruitId);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
