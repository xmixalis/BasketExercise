using BasketApi.Client.Services;

namespace BasketApi.Client
{
    /// <summary>
    /// API client object that gives access to all services supported by the API
    /// </summary>
    public class BasketApiClient
    {
        private BasketService _basketService;
        public BasketService BasketService { get { return _basketService ?? (_basketService = new BasketService(_baseAddress)); } }

        private ProductService _productService;
        public ProductService ProductService { get { return _productService ?? (_productService = new ProductService(_baseAddress)); } }

        private string _baseAddress;

        /// <summary>
        /// Constructor of the API client
        /// </summary>
        /// <param name="baseAddress">Base address of the Basket API</param>
        public BasketApiClient(string baseAddress)
        {
            _baseAddress = baseAddress;
        }


    }
}
