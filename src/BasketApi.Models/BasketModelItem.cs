using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BasketApi.Models
{
    public class BasketModelItem
    {
        [JsonProperty("productid")]
        public int ProductId { get; set; }
        [JsonProperty("productname")]
        public string ProductName { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}
