using DomainLayer.Models.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.Specifications
{
    class OrderSpecifications : BaseSpecification<Order, Guid>
    {
        public OrderSpecifications(string Email) : base(O => O.UserEmail == Email)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
            AddOrderByDescending(O => O.OrderDate);
        }
        public OrderSpecifications(Guid id ) : base(O => O.Id == id) 
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
        }
    }
}
