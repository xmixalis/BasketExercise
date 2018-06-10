using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BasketApi.Infrastructure.Entities
{
    /// <summary>
    /// Base entity
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Primary key of the entity
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
    }
}
