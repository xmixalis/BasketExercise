using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BasketApi.Models
{
    /// <summary>
    /// Object with details for an basket item quantity to be updated
    /// </summary>
    public class BasketUpdateItem
    {
        /// <summary>
        /// Product ID of the products repository
        /// </summary>
        [JsonProperty("productid")]
        public int ProductId { get; set; }
        /// <summary>
        /// New quantity to be set for the product in the basket
        /// </summary>
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}
