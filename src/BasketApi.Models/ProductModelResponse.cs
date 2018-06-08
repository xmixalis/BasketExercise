using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BasketApi.Models
{
    public class ProductModelResponse
    {
        [JsonProperty("productid")]
        public int ProductId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description",NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
    }
}
