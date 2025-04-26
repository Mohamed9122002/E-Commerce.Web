using DomainLayer.Contracts;
using DomainLayer.Models.ProductModel;
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
    public class DataSeeding(StoreDbContext _storeDbContext) : IDataSeeding
    {
        public  async Task DataSeedAsync()
        {
            try
            {
                var pendingMigrations = await _storeDbContext.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                {
                  await  _storeDbContext.Database.MigrateAsync();
                }
                // Read Data From JSON File
                if (!_storeDbContext.ProductBrands.Any())
                {
                    //var ProductBrandData =await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    var ProductBrandData =  File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");

                    // Convert Data "String" to C# Object 
                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandData);
                    // Save Data to Database
                    if (ProductBrands is not null && ProductBrands.Any())
                    {
                       await _storeDbContext.ProductBrands.AddRangeAsync(ProductBrands);
                    }
                }
                if (!_storeDbContext.ProductTypes.Any())
                {
                    var ProductTypeData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");
                    // Convert Data "String" to C# Object 
                    var ProductTypes =  await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypeData);
                    // Save Data to Database
                    if (ProductTypes is not null && ProductTypes.Any())
                    {
                        await _storeDbContext.ProductTypes.AddRangeAsync(ProductTypes);
                    }
                }
                if (!_storeDbContext.Products.Any())
                {
                    var ProductData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");
                    // Convert Data "String" to C# Object 
                    var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductData);
                    // Save Data to Database
                    if (Products is not null && Products.Any())
                    {
                       await _storeDbContext.Products.AddRangeAsync(Products);
                    }
                }
               await _storeDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
