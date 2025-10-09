using DomainLayer.Models.ProductModul;
using Shared;
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
        public ProductWithBrandAndTypeSpecifications(int? BrandId , int? TypeId,ProductSortingOptions sortingOptions)
            : base(p =>(!BrandId.HasValue || p.BrandId == BrandId)&&
            (!TypeId.HasValue || p.TypeId == TypeId))
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(p => p.ProductType);
            switch (sortingOptions)
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
        }
        public ProductWithBrandAndTypeSpecifications(int id ):base(P=>P.Id == id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
