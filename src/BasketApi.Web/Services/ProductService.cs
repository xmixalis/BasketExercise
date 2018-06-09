using System.Collections.Generic;
using System.Threading.Tasks;
using BasketApi.Web.Interfaces;
using BasketApi.Infrastructure.Entities;
using BasketApi.Infrastructure.Interfaces;
using System.Linq.Expressions;
using System;

namespace BasketApi.Web.Services
{
    public class ProductService:IProductService
    {
        private readonly IAsyncRepository<ProductItem> _productRepository;

        public ProductService(IAsyncRepository<ProductItem> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductItem>> GetAllItemsAsync()
        {
            return await _productRepository.ListAllAsync();
        }

        public async Task<IEnumerable<ProductItem>> ListAsync(Expression<Func<ProductItem, bool>> criteria)
        {
            return await _productRepository.ListAsync(criteria);
        }
    }
}
