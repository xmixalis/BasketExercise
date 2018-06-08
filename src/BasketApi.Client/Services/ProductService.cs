using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BasketApi.Client.Helpers;
using BasketApi.Models;

namespace BasketApi.Client.Services
{
    public class ProductService:ServiceBase
    {
        public ProductService(string baseAddress) : base(baseAddress) { }

        public async Task<List<ProductModelResponse>> GetProductsAsync()
        {
            string serviceURI = "api/Product/list";
            return await new ApiHttpClient(_baseAddress).GetAsync<List<ProductModelResponse>>(serviceURI);
        }


    }
}
