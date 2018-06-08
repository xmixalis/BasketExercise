using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketApi.Client;
using BasketApi.Models;
using BasketWebUI.Interfaces;
using BasketWebUI.Models;
using Microsoft.Extensions.Logging;

namespace BasketWebUI.Services
{
    /// <summary>
    /// This is a UI-specific service so belongs in UI project. It does not contain 
    /// any business logic and works with UI-specific types.
    /// </summary>
    public class ProductsService : IProductService
    {
        //depedencies to inject
        private readonly ILogger<ProductsService> _logger;

        //constructor
        public ProductsService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ProductsService>();
        }

        public ProductsIndexViewModel GetProductItems()
        {
            _logger.LogInformation("GetProductItems called.");

            BasketApiClient client = new BasketApiClient("http://localhost:54000");
            IEnumerable<ProductModelResponse> items = client.ProductService.GetProductsAsync().Result;
            
            //imrovment could be to add paging 
            ProductsIndexViewModel result = new ProductsIndexViewModel()
            {
                ProductItems = items.Select(i=> new ProductItemViewModel()
                {
                    Id = i.ProductId,
                    Name = i.Name,
                    Price = i.Price
                })
            };

            return result;
        }
    }
}
