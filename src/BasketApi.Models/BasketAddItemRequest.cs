using Newtonsoft.Json;

namespace BasketApi.Models
{
    /// <summary>
    /// Object with details for an item to be added to the basket
    /// </summary>
    public class BasketAddItemRequest
    {
        /// <summary>
        /// Product ID of the Products repository
        /// </summary>
        [JsonProperty("productid")]
        public int ProductId { get; set; }
        /// <summary>
        /// Price of the product
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity of the product to be ordered
        /// </summary>
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}
