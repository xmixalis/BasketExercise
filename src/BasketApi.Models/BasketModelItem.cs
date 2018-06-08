using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BasketApi.Models
{
    public class BasketModelItem
    {
        [JsonProperty("productid", NullValueHandling = NullValueHandling.Ignore)]
        public int ProductId { get; set; }
        [JsonProperty("productname", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductName { get; set; }
        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Price { get; set; }
        [JsonProperty("quantity", NullValueHandling = NullValueHandling.Ignore)]
        public int Quantity { get; set; }
    }
}
