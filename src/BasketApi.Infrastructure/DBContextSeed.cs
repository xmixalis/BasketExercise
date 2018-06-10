using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketApi.Infrastructure.Entities;
using Microsoft.Extensions.Logging;

namespace BasketApi.Infrastructure
{
    /// <summary>
    /// Class for template data for the prototype
    /// </summary>
    public class DBContextSeed
    {
        public static async Task InitAsync(BasketDbContext dbContext,
            ILoggerFactory loggerFactory)
        {
            if (!dbContext.ProductItems.Any())
            {
                //add some sample data
                dbContext.ProductItems.AddRange(GetPreconfiguredItems());

                await dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Template data for the prototype
        /// </summary>
        /// <returns>Product items template data</returns>
        static IEnumerable<ProductItem> GetPreconfiguredItems()
        {
            return new List<ProductItem>()
            {
                new ProductItem { Description="Product 1", Name="Product 1", Price=10},
                new ProductItem { Description="Product 2", Name="Product 2", Price=20},
                new ProductItem { Description="Product 3", Name="Product 3", Price=30},
                new ProductItem { Description="Product 4", Name="Product 4", Price=40},
                new ProductItem { Description="Product 5", Name="Product 5", Price=50},
                new ProductItem { Description="Product 6", Name="Product 6", Price=60},
                new ProductItem { Description="Product 7", Name="Product 7", Price=70},
                new ProductItem { Description="Product 8", Name="Product 8", Price=80},
                new ProductItem { Description="Product 9", Name="Product 9", Price=90},
                new ProductItem {  Description="Product 10", Name="Product 10", Price=100},
                new ProductItem {  Description="Product 11", Name="Product 11", Price=110},
                new ProductItem {  Description="Product 12", Name="Product 12", Price=120},
                new ProductItem {  Description="Product 13", Name="Product 13", Price=130},
                new ProductItem {  Description="Product 14", Name="Product 14", Price=140},
                new ProductItem {  Description="Product 15", Name="Product 15", Price=150},
                new ProductItem {  Description="Product 16", Name="Product 16", Price=160},
                new ProductItem {  Description="Product 17", Name="Product 17", Price=170},
                new ProductItem {  Description="Product 18", Name="Product 18", Price=180},
                new ProductItem {  Description="Product 19", Name="Product 19", Price=190},
                new ProductItem {  Description="Product 20", Name="Product 20", Price=200},
                new ProductItem {  Description="Product 21", Name="Product 21", Price=210},
                new ProductItem {  Description="Product 22", Name="Product 22", Price=220},
                new ProductItem {  Description="Product 23", Name="Product 23", Price=230},
                new ProductItem {  Description="Product 24", Name="Product 24", Price=240},
                new ProductItem {  Description="Product 25", Name="Product 25", Price=250},
                new ProductItem {  Description="Product 26", Name="Product 26", Price=260},
                new ProductItem {  Description="Product 27", Name="Product 27", Price=270},
                new ProductItem {  Description="Product 28", Name="Product 28", Price=280}
            };
        }

    }
}
