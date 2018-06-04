using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketCore.Entities;
using BasketCore.Interfaces;
using BasketWebApi.Interfaces;
using BasketWebApi.Models;
using Microsoft.Extensions.Logging;

namespace BasketWebApi.Services
{
    /// <summary>
    /// This is a UI-specific service so belongs in UI project. It does not contain 
    /// any business logic and works with UI-specific types.
    /// </summary>
    public class ProductsService : IProductService
    {
        //depedencies to inject
        private readonly ILogger<ProductsService> _logger;
        private readonly IRepository<ProductItem> _itemRepository;

        //constructor
        public ProductsService(
            ILoggerFactory loggerFactory,
            IRepository<ProductItem> itemRepository)
        {
            _logger = loggerFactory.CreateLogger<ProductsService>();
            _itemRepository = itemRepository;
        }

        public ProductsIndexViewModel GetProductItems()
        {
            _logger.LogInformation("GetProductItems called.");

            IEnumerable<ProductItem> items = _itemRepository.ListAll();

            //imrovent could be to add paging 
            ProductsIndexViewModel result = new ProductsIndexViewModel()
            {
                ProductItems = items.Select(i=> new ProductItemViewModel()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Price = i.Price
                })
            };

            return result;
        }
    }
}
