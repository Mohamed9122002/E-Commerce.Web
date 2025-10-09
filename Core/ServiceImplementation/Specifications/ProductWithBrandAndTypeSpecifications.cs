using DomainLayer.Models.ProductModul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.Specifications
{
    public class ProductWithBrandAndTypeSpecifications :BaseSpecification<Product ,int>
    {
        // Get All Products With Brand With Type
        public ProductWithBrandAndTypeSpecifications() :base(null)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(p => p.ProductType);
        }
        public ProductWithBrandAndTypeSpecifications(int id ):base(P=>P.Id == id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
