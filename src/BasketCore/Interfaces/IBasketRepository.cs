using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BasketCore.Entities;

namespace BasketCore.Interfaces
{
    public interface IBasketRepository : IRepository<Basket>, IAsyncRepository<Basket>
    {
        Basket GetByIdWithItems(int id);
        Task<Basket> GetByIdWithItemsAsync(int id);
    }
}
