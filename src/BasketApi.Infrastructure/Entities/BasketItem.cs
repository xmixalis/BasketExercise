using System;
using System.Collections.Generic;
using System.Text;

namespace BasketApi.Infrastructure.Entities
{
    public class BasketItem : BaseEntity
    {
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int ProductItemId { get; set; }
    }
}
