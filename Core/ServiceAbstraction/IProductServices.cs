using Shared;
using Shared.DataTransferObject;
using Shared.Eums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IProductServices
    {
        // get All Product 
        Task<IEnumerable<ProductDtos>> GetAllProductsAsync(ProductQueryParams queryParams );

        // Get All By Id   
        Task<ProductDtos> GetProductByIdAsync(int id);

        // Get All Types 
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();

        // Get All Brands
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
    }
}
