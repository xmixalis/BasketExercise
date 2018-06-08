using System;
using System.Collections.Generic;
using System.Linq;

namespace BasketWebUI.Models
{
    public class BasketIndexViewModel
    {
        public int Id { get; set; }
        public List<BasketItemViewModel> BasketItems { get; set; }
        public string UserId { get; set; }

        public decimal Total()
        {
            return Math.Round(BasketItems.Sum(x => x.UnitPrice * x.Quantity), 2);
        }
    }
}
