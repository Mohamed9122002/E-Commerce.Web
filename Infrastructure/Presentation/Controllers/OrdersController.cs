using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOS.OrderDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class OrdersController(IServiceManager _serviceManager) : APIBaseController
    {
        // Create Order 
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDTo>> CreateOrder(OrderDto orderDto)
        {
            var Order = await _serviceManager.OrderService.CreateOrder(orderDto, GetEmailFromToken());
            return Ok(Order);
        }
    }
}


