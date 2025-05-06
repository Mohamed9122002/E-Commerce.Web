using Shared.DataTransferObject.IdentityDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderDTos
{
    public class OrderDTo
    {
        public string BaskedId { get; set; } = default!;
        public int DeliveryMethodId { get; set; }
       public AddressDTo Address { get; set; } = default!;
    }
}
