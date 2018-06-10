using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BasketApi.Infrastructure.Entities;

namespace BasketApi.Infrastructure.Interfaces
{
    public interface IBasketRepository : IAsyncRepository<Basket>
    {
        Basket GetByIdWithItems(int id);
        Task<Basket> GetByIdWithItemsAsync(int id);
    }
}
