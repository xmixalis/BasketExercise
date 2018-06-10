using Newtonsoft.Json;

namespace BasketApi.Models
{
    /// <summary>
    /// Item that is contained to the list of items in a basket request
    /// </summary>
    public class BasketModelItem
    {
        /// <summary>
        /// Product ID of the Products Product ID of the Products repository
        /// </summary>
        [JsonProperty("productid")]
        public int ProductId { get; set; }
        /// <summary>
        /// Product name of the Products repository
        /// </summary>
        [JsonProperty("productname")]
        public string ProductName { get; set; }
        /// <summary>
        /// Product price of the Products repository
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity of this product in the basket
        /// </summary>
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}
