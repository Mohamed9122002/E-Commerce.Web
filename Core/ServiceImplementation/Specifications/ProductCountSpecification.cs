using DomainLayer.Models.ProductModul;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.Specifications
{
    internal class ProductCountSpecification : BaseSpecification<Product, int>
    {
        public ProductCountSpecification(ProductQueryParameters queryParameters) : base(p => (!queryParameters.BrandId.HasValue || p.BrandId == queryParameters.BrandId) &&
            (!queryParameters.TypeId.HasValue || p.TypeId == queryParameters.TypeId)
            && (string.IsNullOrWhiteSpace(queryParameters.SearchValue) || p.Name.ToLower().Contains(queryParameters.SearchValue.ToLower())))
 
        {

        }
    }
}
