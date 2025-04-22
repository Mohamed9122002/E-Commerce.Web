using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using ServiceAbstraction;
using ServiceImplemention.Specifications;
using Shared;
using Shared.DataTransferObject;
using Shared.Eums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemention
{
    public class ProductServices(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductServices
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var Repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var Brands = await Repo.GetAllAsync();
            // Convert Data(ProductBrand) to DTO
            var brandsDto = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);
            return brandsDto;

        }

        public async Task<PaginatedResult<ProductDtos>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var Repo = _unitOfWork.GetRepository<Product, int>();

            // Create Object ProductWihtBarndAndTypeSpecificaion 
            var Specifications = new ProductWithBrandAndTypeSpecification(queryParams);

            var Products = await Repo.GetAllAsync(Specifications);
            // Convert Data(Product) to DTO
            var Data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDtos>>(Products);
            var ProductCount = Products.Count();
            var TotalCount = await Repo.CountAsync(new ProductCountSpecification(queryParams));
            return new PaginatedResult<ProductDtos>(queryParams.PageIndex, ProductCount, 0, Data);

        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            // Convert Data(ProductType) to DTO
            return _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(Types);
        }

        public async Task<ProductDtos> GetProductByIdAsync(int id)
        {
            var Specifications = new ProductWithBrandAndTypeSpecification(id);
            // Get Product By Id 
            var Product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(Specifications);
            // Convert Data(Product) to DTO
            return _mapper.Map<Product, ProductDtos>(Product);
        }
    }
}
