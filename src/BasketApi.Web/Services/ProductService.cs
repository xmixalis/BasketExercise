using System.Collections.Generic;
using System.Threading.Tasks;
using BasketApi.Web.Interfaces;
using BasketApi.Infrastructure.Entities;
using BasketApi.Infrastructure.Interfaces;

namespace BasketApi.Web.Services
{
    public class ProductService:IProductService
    {
        private readonly IAsyncRepository<ProductItem> _productRepository;

        public ProductService(IAsyncRepository<ProductItem> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductItem>> GetAllItems()
        {
            return await _productRepository.ListAllAsync();
        }
    }
}
