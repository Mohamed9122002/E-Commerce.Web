using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModule
{
    public class Order :BaseEntity<Guid>
    {
        public string UserEmail { get; set; } = default!;
        public  DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public OrderAddress OrderAddress { get; set; } = default!;
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public int DeliveryMethodId { get; set; } = default!; // Fk 

        public OrderStatus OrderStatus { get; set; } 

        public ICollection<OrderItem> OrderItems { get; set; } = [];
        public decimal SubTotal { get; set; }
        
        public decimal GetTotal()
        {
            return SubTotal + DeliveryMethod.Price;
        }
    }
}
