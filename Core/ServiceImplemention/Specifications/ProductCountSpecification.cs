using DomainLayer.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemention.Specifications
{
    class ProductCountSpecification : BaseSpecifications<Product, int>
    {
        public ProductCountSpecification(ProductQueryParams productQueryParams)
              : base(P => (!productQueryParams.BrandId.HasValue || P.BrandId == productQueryParams.BrandId) && (!productQueryParams.TypeId.HasValue || P.TypeId == productQueryParams.TypeId) && (string.IsNullOrWhiteSpace(productQueryParams.SearchValue) || P.Name.ToLower().Contains(productQueryParams.SearchValue.ToLower())))
        {


        }
    }
}
