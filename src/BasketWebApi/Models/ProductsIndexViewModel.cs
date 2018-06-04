using System.Collections.Generic;

namespace BasketWebApi.Models
{
    public class ProductsIndexViewModel
    {
        public IEnumerable<ProductItemViewModel> ProductItems { get; set; }
        //public string TestMessage => "test message";
    }
}
