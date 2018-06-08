using System;
using System.Collections.Generic;
using System.Text;

namespace BasketApi.Models
{
    public class ProductModelResponse
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
