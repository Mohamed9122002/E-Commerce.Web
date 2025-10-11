using Shared.DTOS.AuthDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.OrderDTOS
{
    public class OrderDto
    {
        public string BasketId { get; set; } = default!;
         public int DeliveryMethodId { get; set; }
        public AddressDto Address { get; set; } = default!;
    }
}
