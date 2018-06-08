using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketApi.Infrastructure.Entities;
using Microsoft.Extensions.Logging;

namespace BasketApi.Infrastructure
{
    public class DBContextInit
    {
        public static async Task InitAsync(BasketDbContext dbContext, 
            ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            try
            {
                if (!dbContext.ProductItems.Any())
                {
                    //add some sample data
                    dbContext.ProductItems.AddRange( GetPreconfiguredItems() );

                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<DBContextInit>();
                    log.LogError(ex.Message);
                    await InitAsync(dbContext, loggerFactory, retryForAvailability);
                }
            }
        }

        static IEnumerable<ProductItem> GetPreconfiguredItems()
        {
            return new List<ProductItem>()
            {
                new ProductItem { Id=1, Description="Product 1", Name="Product 1", Price=10},
                new ProductItem { Id=2, Description="Product 2", Name="Product 2", Price=20},
                new ProductItem { Id=3, Description="Product 3", Name="Product 3", Price=30},
                new ProductItem { Id=4, Description="Product 4", Name="Product 4", Price=40},
                new ProductItem { Id=5, Description="Product 5", Name="Product 5", Price=50},
                new ProductItem { Id=6, Description="Product 6", Name="Product 6", Price=60},
                new ProductItem { Id=7, Description="Product 7", Name="Product 7", Price=70},
                new ProductItem { Id=8, Description="Product 8", Name="Product 8", Price=80},
                new ProductItem { Id=9, Description="Product 9", Name="Product 9", Price=90},
                new ProductItem { Id=10, Description="Product 10", Name="Product 10", Price=100},
                new ProductItem { Id=11, Description="Product 11", Name="Product 11", Price=110},
                new ProductItem { Id=12, Description="Product 12", Name="Product 12", Price=120},
                new ProductItem { Id=13, Description="Product 13", Name="Product 13", Price=130},
                new ProductItem { Id=14, Description="Product 14", Name="Product 14", Price=140},
                new ProductItem { Id=15, Description="Product 15", Name="Product 15", Price=150},
                new ProductItem { Id=16, Description="Product 16", Name="Product 16", Price=160},
                new ProductItem { Id=17, Description="Product 17", Name="Product 17", Price=170},
                new ProductItem { Id=18, Description="Product 18", Name="Product 18", Price=180},
                new ProductItem { Id=19, Description="Product 19", Name="Product 19", Price=190},
                new ProductItem { Id=20, Description="Product 20", Name="Product 20", Price=200},
                new ProductItem { Id=21, Description="Product 21", Name="Product 21", Price=210},
                new ProductItem { Id=22, Description="Product 22", Name="Product 22", Price=220},
                new ProductItem { Id=23, Description="Product 23", Name="Product 23", Price=230},
                new ProductItem { Id=24, Description="Product 24", Name="Product 24", Price=240},
                new ProductItem { Id=25, Description="Product 25", Name="Product 25", Price=250},
                new ProductItem { Id=26, Description="Product 26", Name="Product 26", Price=260},
                new ProductItem { Id=27, Description="Product 27", Name="Product 27", Price=270},
                new ProductItem { Id=28, Description="Product 28", Name="Product 28", Price=280}
            };
        }

    }
}
