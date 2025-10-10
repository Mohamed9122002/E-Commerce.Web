using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.ProductModul;
using ServiceAbstraction;
using ServiceImplementation.Specifications;
using Shared;
using Shared.DTOS;

namespace ServiceImplementation
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var Repository = _unitOfWork.GetRepository<ProductBrand, int>();
            var Brands = await Repository.GetAllAsync();
            //Mapping ProductBrand To BrandDto
            return _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var Repository = _unitOfWork.GetRepository<ProductType, int>();
            var Types = await Repository.GetAllAsync();
            //Mapping ProductType To TypeDto
            return _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(Types);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var specifications = new ProductWithBrandAndTypeSpecifications(id);
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(specifications);
           if(product is null)
            {
                throw new ProductNotFound(id);
            }
            var ProductDto = _mapper.Map<Product, ProductDto>(product);
            return ProductDto;
        }
        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParameters queryParameters)
        {
            var repo =  _unitOfWork.GetRepository<Product, int>();
            var specifications = new ProductWithBrandAndTypeSpecifications(queryParameters);
            var Products = await repo.GetAllAsync(specifications);
            var Count =  Products.Count();
            var TotalCount = await repo.CountAsync(new ProductCountSpecification(queryParameters));
            var Data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);
            return new PaginatedResult<ProductDto>(queryParameters.PageIndex, Count, TotalCount, Data);
        }
    }
}
