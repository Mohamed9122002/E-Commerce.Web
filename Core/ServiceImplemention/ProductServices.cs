using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using ServiceAbstraction;
using ServiceImplemention.Specifications;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemention
{
    public class ProductServices(IUnitOfWork _unitOfWork, IMapper _mapper ) : IProductServices
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var Repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var Brands = await Repo.GetAllAsync();
            // Convert Data(ProductBrand) to DTO
            var brandsDto = _mapper.Map<IEnumerable<ProductBrand>,IEnumerable<BrandDto>>(Brands);
            return brandsDto;

        }

        public async Task<IEnumerable<ProductDtos>> GetAllProductsAsync()
        {
            // Create Object ProductWihtBarndAndTypeSpecificaion 
            var Specifications = new ProductWithBrandAndTypeSpecification ();

            var Products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(Specifications);
            // Convert Data(Product) to DTO
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDtos>>(Products);

        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var Types =  await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
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
