using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.BaskedModule
{
    public class CustomerBasket
    {
        public string Id { get; set; } // Guid
        public ICollection<BasketItem> BasketItems { get; set; } = [];
     }
}
