using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasketApi.Infrastructure.Entities
{
    /// <summary>
    /// Basket database entity
    /// </summary>
    public class Basket : BaseEntity
    {
        private readonly List<BasketItem> _items = new List<BasketItem>();

        /// <summary>
        /// User ID connected with the basket
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// List of the items that are contained in the basket.
        /// It is readonly so that we apply specific rules when performing CRUD operations 
        /// </summary>
        public IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();

        /// <summary>
        /// Add a new item in the basket.
        /// If the item exists then the quantity is increased
        /// </summary>
        /// <param name="productItemId">Product ID of the item</param>
        /// <param name="unitPrice">Unit price of the item</param>
        /// <param name="quantity">Quantity of the item for the basket</param>
        public void AddItem(int productItemId, decimal unitPrice, int quantity = 1)
        {
            if (!Items.Any(i => i.ProductItemId == productItemId))
            {
                BasketItem b = new BasketItem()
                {
                    ProductItemId = productItemId,
                    Quantity = quantity,
                    UnitPrice = unitPrice
                };
                _items.Add(b);
                return;
            }
            var existingItem = Items.FirstOrDefault(i => i.ProductItemId == productItemId);
            existingItem.Quantity += quantity;
        }

        /// <summary>
        /// Remove an existing item from the basket
        /// </summary>
        /// <param name="productItemId">Product ID of the item to be removed</param>
        public void RemoveItem(int productItemId)
        {
            BasketItem existingItem = Items.FirstOrDefault(i => i.ProductItemId == productItemId);
            if (existingItem != null)
                _items.Remove(existingItem);
        }

        /// <summary>
        /// Clears all items of the basket
        /// </summary>
        public void ClearItems()
        {
            _items.Clear();
        }

    }

}
