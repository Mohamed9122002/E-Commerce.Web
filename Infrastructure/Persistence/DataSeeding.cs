using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModul;
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
    public class DataSeeding(StoreDbContext _dbContext, IdentityStoreDbContext _identityStore, UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager) : IDataSeeding
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
                    using var productBrandData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    // convert Data "string" => C# Object [ProductBrand]
                    var productBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productBrandData);
                    if (productBrands is not null && productBrands.Any())
                    {
                        await _dbContext.ProductBrands.AddRangeAsync(productBrands);
                    }
                }
                if (!_dbContext.ProductTypes.Any())
                {
                    using var productTypeData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");
                    // convert Data "string" => C# Object [ProductBrand]
                    var productTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypeData);
                    if (productTypes is not null && productTypes.Any())
                    {
                        await _dbContext.ProductTypes.AddRangeAsync(productTypes);
                    }
                }
                if (!_dbContext.Products.Any())
                {
                    using var productData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");
                    // convert Data "string" => C# Object [ProductBrand]
                    var products = await JsonSerializer.DeserializeAsync<List<Product>>(productData);
                    if (products is not null && products.Any())
                    {
                        await _dbContext.Products.AddRangeAsync(products);
                    }
                }
                if (!_dbContext.Set<DeliveryMethod>().Any())
                {
                    using var DeliveryMethodData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\delivery.json");
                    // convert Data "string" => C# Object [ProductBrand]
                    var DeliveryMethods = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(DeliveryMethodData);
                    if (DeliveryMethods is not null && DeliveryMethods.Any())
                    {
                        await _dbContext.Set<DeliveryMethod>().AddRangeAsync(DeliveryMethods);
                    }
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // ToDo 
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
                        Email = "mohamedmahamoudali15@gmail.com",
                        DisplayName = "Mohamed Mahmoud",
                        PhoneNumber = "01098132487",
                        UserName = "Mohamed912"
                    };
                    var User02 = new ApplicationUser()
                    {
                        Email = "mohamedmahamoufsdfsdfdali15@gmail.com",
                        DisplayName = "Mohamed Ahmed",
                        PhoneNumber = "01091132487",
                        UserName = "Mohamed9"
                    };
                    await _userManager.CreateAsync(User01, "P@ssw0rd");
                    await _userManager.CreateAsync(User02, "P@ssw0rd12");
                    await _userManager.AddToRoleAsync(User01, "SuperAdmin");
                    await _userManager.AddToRoleAsync(User02, "Admin");
                }
                await _identityStore.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
