using Shared.OrderDTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IOrderServices
    {
        //Create Order
        //Creating Order Will Take Basket Id , Shipping Address ,Delivery Method Id , Customer Email
        //And Return Order Details
        //(Id , UserName , OrderDate , Items (Product Name - Picture Url - Price - Quantity) , Address ,
        //Delivery Method Name , Order Status Value , Sub Total , Total Price  )

        Task<OrderToReturnDTo> CreateOrderAsync(OrderDTo orderDTo ,string Email);

    }
}
