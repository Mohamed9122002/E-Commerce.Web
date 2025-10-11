using Shared.DTOS.OrderDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IOrderService
    {
        // Create Order 
        // take BaskedID, Shipping Address , Delivery Method ID , Customer Email , 
        // return Id , UserEmail , Order Date , Items ,Address ,Delivery Method ,Order Status ,SuTotal ,TotalPrice ,
        Task<OrderToReturnDTo> CreateOrder(OrderDto orderDto,string Email);
    }
}
