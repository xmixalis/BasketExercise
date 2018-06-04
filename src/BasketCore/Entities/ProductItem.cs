﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BasketCore.Entities
{
    public class ProductItem : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
