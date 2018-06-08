using BasketApi.Client.Services;

namespace BasketApi.Client
{
    public class BasketApiClient
    {
        private BasketService _basketService;
        public BasketService BasketService { get { return _basketService ?? (_basketService = new BasketService(_baseAddress)); } }

        private ProductService _productService;
        public ProductService ProductService { get { return _productService ?? (_productService = new ProductService(_baseAddress)); } }

        private string _baseAddress;

        public BasketApiClient(string baseAddress)
        {
            _baseAddress = baseAddress;
        }


    }
}
