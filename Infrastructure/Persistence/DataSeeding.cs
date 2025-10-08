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
        public async Task DataSeedAsync()
        {
            var PendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
            try
            {
                if (PendingMigrations.Any())
                {
                   await _dbContext.Database.MigrateAsync();
                }
                if (!_dbContext.ProductBrands.Any())
                {
                    var productBrandData =  File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    // convert Data "string" => C# Object [ProductBrand]
                    var productBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productBrandData);
                    if (productBrands is not null && productBrands.Any())
                    {
                      await  _dbContext.ProductBrands.AddRangeAsync(productBrands);
                    }
                }
                if (!_dbContext.ProductTypes.Any())
                {
                    var productTypeData =  File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");
                    // convert Data "string" => C# Object [ProductBrand]
                    var productTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypeData);
                    if (productTypes is not null && productTypes.Any())
                    {
                      await  _dbContext.ProductTypes.AddRangeAsync(productTypes);
                    }
                }
                if (!_dbContext.Products.Any())
                {
                    var productData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");
                    // convert Data "string" => C# Object [ProductBrand]
                    var products = await JsonSerializer.DeserializeAsync<List<Product>>(productData);
                    if (products is not null && products.Any())
                    {
                      await  _dbContext.Products.AddRangeAsync(products);
                    }
                }
                 await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // ToDo 
            }

        }

    }
}
