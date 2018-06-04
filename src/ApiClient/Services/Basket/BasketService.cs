using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApiClient;

namespace BasketApiClient.Services.Basket
{
    public class BasketService
    {
        public Task<string> GetBasket(string basketId)
        {
            string serviceURI = "";
            var getBasketUri = string.Format(serviceURI, basketId);
            return new ApiServiceClient().GetAsync(getBasketUri);
        }
    }
}