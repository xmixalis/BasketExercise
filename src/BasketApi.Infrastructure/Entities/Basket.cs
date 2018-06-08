using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasketApi.Infrastructure.Entities
{
    public class Basket : BaseEntity
    {
        public string UserId { get; set; }
        private readonly List<BasketItem> _items = new List<BasketItem>();
        public IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();

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

        public void RemoveItem(int productItemId)
        {
            BasketItem existingItem = Items.FirstOrDefault(i => i.ProductItemId == productItemId);
            if (existingItem != null)
                _items.Remove(existingItem);
        }

        public void ClearItems()
        {
            _items.Clear();
        }

    }

}
