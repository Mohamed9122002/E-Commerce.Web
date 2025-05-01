using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObject;
using Shared.Eums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{

    public  class ProductsController(IServiceManager  _serviceManager): ApiBaseController
    {
        // Get All Prdoucts 
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDtos>>> GetAllProducts([FromQuery]ProductQueryParams queryParams)
        {
           var Products =  await  _serviceManager.ProductServices.GetAllProductsAsync(queryParams);
             
            if (Products == null)
            {
                return NotFound();
            }
            return Ok(Products);
            
        }
        // Get Product By Id
        [HttpGet("{id}")]
        // GET BaseUrl/api/Products/10
        public async Task<ActionResult<ProductDtos>> GetProductById(int id)
        {
            var product = await _serviceManager.ProductServices.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        // Get All Types 
        [HttpGet("Types")]
        public  async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes()
        {
            var Types = await _serviceManager.ProductServices.GetAllTypesAsync();

            if (Types == null)
            {
                return NotFound();
            }
            return Ok(Types);
        }
        // Get All Brands
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
        {
            var Brands = await _serviceManager.ProductServices.GetAllBrandsAsync();
            if (Brands == null)
            {
                return NotFound();
            }
            return Ok(Brands);
        }

    }
}
