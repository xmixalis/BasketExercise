using Newtonsoft.Json;

namespace BasketApi.Models
{
    public class BasketAddItemRequest
    {
        [JsonProperty("productid")]
        public int ProductId { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}
