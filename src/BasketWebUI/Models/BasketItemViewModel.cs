using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketWebUI.Models
{
    public class BasketItemViewModel
    {
        //public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int ProductItemId { get; set; }
    }
}
