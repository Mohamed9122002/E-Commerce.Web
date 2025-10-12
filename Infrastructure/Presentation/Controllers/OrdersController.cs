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
        // GetAllOrder 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDTo>>> GetAllOrders()
        {
            var Orders = await _serviceManager.OrderService.GetAllOrdersAsync(GetEmailFromToken());
            return Ok(Orders);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<OrderToReturnDTo>> GetOrderById(Guid Id)
        {
            var Order = await _serviceManager.OrderService.GetAllOrderById(Id);
            return Ok(Order);
        }
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDTO>>> GetAllDeliveryMethod()
        {
            var deliveryMethods = await _serviceManager.OrderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }
    }
}


