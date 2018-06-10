using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasketApi.Infrastructure.Entities
{
    /// <summary>
    /// Product item database entity
    /// </summary>
    public class ProductItem : BaseEntity
    {
        /// <summary>
        /// Name of the product
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Description of the product
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Unit price of the product
        /// </summary>
        [Required]
        public decimal Price { get; set; }
    }
}
