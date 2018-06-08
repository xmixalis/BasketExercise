using System;
using System.Collections.Generic;
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
        public int Id { get; set; }
    }
}
