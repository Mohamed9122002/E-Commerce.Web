using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.DbContexts;
using Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _storeDbContext, UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager ,StoreIdentityDbcontext _storeIdentity) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            try
            {
                var pendingMigrations = await _storeDbContext.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                {
                    await _storeDbContext.Database.MigrateAsync();
                }
                // Read Data From JSON File
                if (!_storeDbContext.ProductBrands.Any())
                {
                    //var ProductBrandData =await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                  using  var ProductBrandData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");

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
                   using var ProductTypeData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");
                    // Convert Data "String" to C# Object 
                    var ProductTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypeData);
                    // Save Data to Database
                    if (ProductTypes is not null && ProductTypes.Any())
                    {
                        await _storeDbContext.ProductTypes.AddRangeAsync(ProductTypes);
                    }
                }
                if (!_storeDbContext.Products.Any())
                {
                  using var ProductData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");
                    // Convert Data "String" to C# Object 
                    var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductData);
                    // Save Data to Database
                    if (Products is not null && Products.Any())
                    {
                        await _storeDbContext.Products.AddRangeAsync(Products);
                    }
                }
                if (!_storeDbContext.Set<DeliveryMethod>().Any())
                {
                    using var DeliveryMethodStream = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\delivery.json");
                    // Convert Data "String" to C# Object 
                    var DeliveryMethods = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(DeliveryMethodStream);
                    // Save Data to Database
                    if (DeliveryMethods is not null && DeliveryMethods.Any())
                    {
                        await _storeDbContext.Set<DeliveryMethod>().AddRangeAsync(DeliveryMethods);
                    }
                }
                await _storeDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task IdentityDataSeedAsync()
        {
           try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser()
                    {
                        Email = "MohamedMahmoud@gmail.com",
                        DisplayName = "Mohamed Mahmoud",
                        PhoneNumber = "01012345678",
                        UserName = "MohamedMahmoud",
                    };
                    var User02 = new ApplicationUser()
                    {
                        Email = "Salma@gmail.com",
                        DisplayName = "Salma Mahmoud",
                        PhoneNumber = "01012315678",
                        UserName = "SalmaMahmoud",
                    };
                    await _userManager.CreateAsync(User01, "P@ssw0rd");
                    await _userManager.CreateAsync(User02, "P@ssw0rd");
                    await _userManager.AddToRoleAsync(User01, "Admin");
                    await _userManager.AddToRoleAsync(User02, "SuperAdmin");
                }

               await  _storeIdentity.SaveChangesAsync();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
