using FruitShop.V1.Controllers.Fruits.Request;
using FruitShop.V1.Controllers.Fruits.Response;
using FruitShop.V1.Controllers.Fruits.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FruitShop.V1.Controllers.Fruits
{
    [Route("api/v1/fruits")]
    [ApiController]
    public class FruitController
    {
        private readonly IFruitService _fruitService;

        public FruitController(IFruitService fruitService)
        {
            _fruitService = fruitService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<FruitResponse> Submit([FromBody] FruitRequest fruitRequest)
        {
            var result = _fruitService.Add(fruitRequest);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<FruitResponse>> GetAll()
        {
            var result = _fruitService.GetAll();
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpGet("{fruitId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<FruitResponse> GetFruit(int fruitId)
        {
            var result = _fruitService.GetFruit(fruitId);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpDelete("{fruitId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<FruitResponse> Remove(int fruitId)
        {
            var result = _fruitService.Remove(fruitId);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
