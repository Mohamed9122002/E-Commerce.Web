using DomainLayer.Contracts;
using DomainLayer.Models;
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
        public void DataSeed()
        {
            try
            {
                if (_storeDbContext.Database.GetPendingMigrations().Any())
                {
                    _storeDbContext.Database.Migrate();
                }
                // Read Data From JSON File
                if (!_storeDbContext.ProductBrands.Any())
                {
                    var ProductBrandData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    // Convert Data "String" to C# Object 
                    var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandData);
                    if (ProductBrands is not null && ProductBrands.Any())
                    {
                        _storeDbContext.ProductBrands.AddRange(ProductBrands);
                    }
                }
                if (!_storeDbContext.ProductTypes.Any())
                {
                    var ProductTypeData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");
                    // Convert Data "String" to C# Object 
                    var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypeData);
                    if (ProductTypes is not null && ProductTypes.Any())
                    {
                        _storeDbContext.ProductTypes.AddRange(ProductTypes);
                    }
                }
                if (!_storeDbContext.Products.Any())
                {
                    var ProductData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");
                    // Convert Data "String" to C# Object 
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                    if (Products is not null && Products.Any())
                    {
                        _storeDbContext.Products.AddRange(Products);
                    }
                }

                // Save Data to Database
                _storeDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
