using System.Collections.Generic;
using System.Threading.Tasks;
using BasketApi.Models;
using BasketApi.Web.Interfaces;
using BasketApi.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using BasketApi.Web.ModelConverters;
using System.Linq;

namespace BasketApi.Web.Controllers
{
    [Route("api/Product")]
    public class ProductApiController : Controller
    {
        private readonly IProductService _productsService;

        public ProductApiController(IProductService productService)
        {
            _productsService = productService;
        }

        [HttpGet("list")]
        public async Task<List<ProductModelResponse>> GetProducts()
        {
            List<ProductItem> productItems = await _productsService.GetAllItems();
            List<ProductModelResponse> response = productItems.Select(p => p.ToProductModelResponse()).ToList();
            return response;
        }
    }
}
