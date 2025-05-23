﻿using DomainLayer.Models.ProductModel;
using Shared;
using Shared.Eums;
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
        public ProductWithBrandAndTypeSpecification(ProductQueryParams queryParams) :
            base(P => (!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId) && (!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId) && (string.IsNullOrWhiteSpace(queryParams.SearchValue) ||P.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
        // P=> P.BrandId
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            switch (queryParams.SortingOptions)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderyByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderyByDescending(p => p.Price);
                    break;
                default:
                    break;

                    
            }


            ApplyPagination( queryParams.PageSize,  queryParams.PageIndex );
        }
        // Get By Id 
        public ProductWithBrandAndTypeSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }

    }
}
