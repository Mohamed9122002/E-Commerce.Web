using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.ProductModul;
using ServiceAbstraction;
using ServiceImplementation.Specifications;
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
            
            return _mapper.Map<Product, ProductDto>(product);
        }
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(int? BrandId, int? TypeId)
        {
            var specifications = new ProductWithBrandAndTypeSpecifications(BrandId,TypeId);
            var Products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(specifications);
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);
        }
    }
}
