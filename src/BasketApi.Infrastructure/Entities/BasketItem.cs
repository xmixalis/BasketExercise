using System;
using System.Collections.Generic;
using System.Text;

namespace BasketApi.Infrastructure.Entities
{
    /// <summary>
    /// Basket item database entity
    /// </summary>
    public class BasketItem : BaseEntity
    {
        /// <summary>
        /// Unit price
        /// </summary>
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// Quantity in the basket
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Product ID of the item
        /// </summary>
        public int ProductItemId { get; set; }
    }
}
