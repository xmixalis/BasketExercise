using System;
using System.Collections.Generic;
using System.Text;

namespace BasketApi.Models
{
    public class BasketAddItemRequest
    {
        //public int BasketId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
