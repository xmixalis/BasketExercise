using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BasketApi.Infrastructure.Entities;

namespace BasketApi.Infrastructure.Interfaces
{
    /// <summary>
    /// Interface for Basket entity specific actions
    /// </summary>
    public interface IBasketRepository : IAsyncRepository<Basket>
    {
        /// <summary>
        /// Retrieves a full basket entity with the items included
        /// </summary>
        /// <param name="id">Basket ID</param>
        /// <returns>Basket entity with the items</returns>
        Task<Basket> GetByIdWithItemsAsync(int id);
    }
}
