using System;
using BasketApi.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketApi.Infrastructure
{
    /// <summary>
    /// Class with the entities supported by the Basket database
    /// </summary>
    public class BasketDbContext : DbContext
    {
        public BasketDbContext(DbContextOptions<BasketDbContext> options) : base(options)
        {
        }

        public DbSet<Basket> Baskets { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Basket>(ConfigureBasket);
            builder.Entity<ProductItem>(ConfigureProductItem);
        }

        private void ConfigureBasket(EntityTypeBuilder<Basket> builder)
        {
            builder.ToTable("Basket");

            var navigation = builder.Metadata.FindNavigation(nameof(Basket.Items));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
        
        private void ConfigureProductItem(EntityTypeBuilder<ProductItem> builder)
        {
            builder.ToTable("Product");
        }
    }

}
