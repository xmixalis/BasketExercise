using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BasketApi.Models
{
    public class BasketUpdateItem
    {
        [JsonProperty("productid", NullValueHandling = NullValueHandling.Ignore)]
        public int ProductId { get; set; }
        [JsonProperty("quantity", NullValueHandling = NullValueHandling.Ignore)]
        public int Quantity { get; set; }
    }
}
