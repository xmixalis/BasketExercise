using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BasketApi.Client.Helpers;
using BasketApi.Models;

namespace BasketApi.Client.Services
{
    /// <summary>
    /// Class that serves requests supported for Products
    /// </summary>
    public class ProductService:ServiceBase
    {
        public ProductService(string baseAddress) : base(baseAddress) { }

        /// <summary>
        /// Lits all the products
        /// </summary>
        /// <returns>List with the existing ptoducts</returns>
        public async Task<List<ProductModelResponse>> GetProductsAsync()
        {
            return await new ApiHttpClient(_baseAddress).GetAsync<List<ProductModelResponse>>(UriHelpers.ProductListUri);
        }


    }
}
