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
    /// <summary>
    /// API controller for product operations
    /// </summary>
    [Route("api/Product")]
    public class ProductApiController : Controller
    {
        private readonly IProductService _productsService;

        public ProductApiController(IProductService productService)
        {
            _productsService = productService;
        }

        /// <summary>
        /// Lists all products of the database
        /// </summary>
        /// <returns>List with details of products</returns>
        [HttpGet("list")]
        public async Task<List<ProductModelResponse>> GetProducts()
        {
            List<ProductItem> productItems = await _productsService.GetAllItemsAsync();
            List<ProductModelResponse> response = productItems.Select(p => p.ToProductModelResponse()).ToList();
            return response;
        }
    }
}
