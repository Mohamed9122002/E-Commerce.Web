using DomainLayer.Models.ProductModul;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.DbContexts
{
    public class StoreDbContext(DbContextOptions<StoreDbContext> dbContextOptions) :DbContext(dbContextOptions)
    {
        public DbSet<Product> Products { get; set; } 
        public DbSet<ProductBrand> ProductBrands { get; set; } 
        public DbSet <ProductType> ProductTypes     { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);
        }
    }
}
