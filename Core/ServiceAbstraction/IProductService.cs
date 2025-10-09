using Shared;
using Shared.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        // Get All Products 
        Task<IEnumerable<ProductDto>> GetAllProductsAsync(ProductQueryParameters queryParameters);
        // Get Product By Id 
        Task<ProductDto> GetProductByIdAsync(int id);
        // Get All Brands 
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
        // Get All By Types 
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
    }
}
