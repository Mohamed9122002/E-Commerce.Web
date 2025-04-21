using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemention.Specifications
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecifications<Product, int>
    {
        // Get All Products With Brand and Type
        public ProductWithBrandAndTypeSpecification(int? BrandId, int? TypeId) :
            base(P => (!BrandId.HasValue || P.BrandId == BrandId)  && (!TypeId.HasValue || P.TypeId == TypeId))
        // P=> P.BrandId
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
        // Get By Id 
        public ProductWithBrandAndTypeSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }

    }
}
