using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BasketApi.Models
{
    public class BasketRemoveItemRequest
    {
        [JsonProperty("productid", NullValueHandling = NullValueHandling.Ignore)]
        public int ProductId { get; set; }
    }
}
