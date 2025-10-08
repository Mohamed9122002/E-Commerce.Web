using DomainLayer.Contracts;
using DomainLayer.Models.ProductModul;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
        public void DataSeed()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    _dbContext.Database.Migrate();
                }
                if (!_dbContext.ProductBrands.Any())
                {
                    var productBrandData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    // convert Data "string" => C# Object [ProductBrand]
                    var productBrands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrandData);
                    if (productBrands is not null && productBrands.Any())
                    {
                        _dbContext.ProductBrands.AddRange(productBrands);
                    }
                }
                if (!_dbContext.ProductTypes.Any())
                {
                    var productTypeData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");
                    // convert Data "string" => C# Object [ProductBrand]
                    var productTypes = JsonSerializer.Deserialize<List<ProductType>>(productTypeData);
                    if (productTypes is not null && productTypes.Any())
                    {
                        _dbContext.ProductTypes.AddRange(productTypes);
                    }
                }
                if (!_dbContext.Products.Any())
                {
                    var productData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");
                    // convert Data "string" => C# Object [ProductBrand]
                    var products = JsonSerializer.Deserialize<List<Product>>(productData);
                    if (products is not null && products.Any())
                    {
                        _dbContext.Products.AddRange(products);
                    }
                }
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // ToDo 
            }

        }

    }
}
