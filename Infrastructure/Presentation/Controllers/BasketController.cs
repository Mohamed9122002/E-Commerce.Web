using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObject.BasketModuleDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{

    public class BasketController(IServiceManager _serviceManager) : ApiBaseController
    {
        // Get Baket 
        [HttpGet] // get/BaseUrl/api/Basket
        public async Task<ActionResult<BasketDTo>> GetBasket(string key)
        {
            var Basket = await _serviceManager.BasketServices.GetBasketAsync(key);
            return Ok(Basket);
        }
        // Create Basket or Updated 
        [HttpPost]
        public async Task<ActionResult<BasketDTo>> CreateOrUpdateBasket( BasketDTo basket)
        {
            var Basket = await _serviceManager.BasketServices.CreateORUpdateBasketAsync(basket);
            return Ok(Basket);
        }
        // Delete Basket 
        [HttpDelete("{key}")] //Delete/BaseUrl/api/Basket/{key}
        public async Task<ActionResult<bool>> DeleteBasket(string key)
        {
            var result = await _serviceManager.BasketServices.DeleteBasketAsync(key);
                return Ok(result);
      
        }
    }
}
