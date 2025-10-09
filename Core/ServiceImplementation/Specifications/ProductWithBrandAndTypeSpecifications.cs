using DomainLayer.Models.ProductModul;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecification<Product, int>
    {
        // Get All Products With Brand With Type
        public ProductWithBrandAndTypeSpecifications(ProductQueryParameters queryParameters)
            : base(p => (!queryParameters.BrandId.HasValue || p.BrandId == queryParameters.BrandId) &&
            (!queryParameters.TypeId.HasValue || p.TypeId == queryParameters.TypeId)
            && (string.IsNullOrWhiteSpace(queryParameters.SearchValue)||p.Name.ToLower().Contains(queryParameters.SearchValue.ToLower())))
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(p => p.ProductType);
            switch (queryParameters.SortingOptions)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    break;
            }
            ApplyPagination(queryParameters.PageSize,queryParameters.PageIndex);

        }
        public ProductWithBrandAndTypeSpecifications(int id ):base(P=>P.Id == id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
