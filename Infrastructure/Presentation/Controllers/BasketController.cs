using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOS.BasketDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BasketController(IServiceManager _serviceManager) : ControllerBase
    {
        // Get Basket  
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string Id)
        {
            var Basket = await _serviceManager.BasketService.GetBasketAsync(Id);
            return Ok(Basket);
        }
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatedBasket(BasketDto basket)
        {
            var Basket = await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }
        [HttpDelete("{Key}")]
        public async Task<ActionResult<bool>> DeleteBasket (string Key)
        {
            var Result = await _serviceManager.BasketService.DeleteBasketAsync(Key);
            return Ok(Result);

        }

    }
}
