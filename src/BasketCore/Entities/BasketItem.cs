using System;
using System.Collections.Generic;
using System.Text;

namespace BasketCore.Entities
{
    public class BasketItem : BaseEntity
    {
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int ProductItemId { get; set; }
    }
}
