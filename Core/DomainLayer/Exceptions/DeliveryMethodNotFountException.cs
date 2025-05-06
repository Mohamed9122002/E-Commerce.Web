using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class DeliveryMethodNotFountException(int id) : NotFoundException($"Delivery Method Not Found With Id ={id}")
    {

    }
}
