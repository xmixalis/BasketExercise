using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BasketApi.Models
{
    /// <summary>
    /// Object with details of a repository item
    /// </summary>
    public class ProductModelResponse
    {
        /// <summary>
        /// Product ID from the products repository
        /// </summary>
        [JsonProperty("productid")]
        public int ProductId { get; set; }
        /// <summary>
        /// Name of the product
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Description of the product
        /// </summary>
        [JsonProperty("description",NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// Unit price of the product
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }
    }
}
