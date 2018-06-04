using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketCore.Entities;
using BasketCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos
{
    public class BasketRepository : BaseRepository<Basket>, IBasketRepository
    {
        public BasketRepository(BasketDbContext dbContext) : base(dbContext)
        {
            
        }

        public Basket GetByIdWithItems(int id)
        {
            //return _dbContext.Baskets
            //    .Where(b=>b.Id == id)
             //   .FirstOrDefault();

            return GetByIdAsync(id).Result;
        }

        public Task<Basket> GetByIdWithItemsAsync(int id)
        {
            return _dbContext.Baskets
                .Where(b => b.Id == id)
                //.Include(o => o.OrderItems)
                //.Include($"{nameof(Order.OrderItems)}.{nameof(OrderItem.ItemOrdered)}")
                .FirstOrDefaultAsync();
        }
    }

}
