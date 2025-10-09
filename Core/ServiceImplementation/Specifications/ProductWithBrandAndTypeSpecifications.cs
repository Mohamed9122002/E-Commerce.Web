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
        public ProductWithBrandAndTypeSpecifications(int? BrandId , int? TypeId)
            : base(p =>(!BrandId.HasValue || p.BrandId == BrandId)&&
            (!TypeId.HasValue || p.TypeId == TypeId))
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
