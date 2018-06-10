using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketApi.Infrastructure.Entities;
using BasketApi.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BasketApi.Infrastructure.Repos
{
    public class BasketRepository : BaseRepository<Basket>, IBasketRepository
    {
        public BasketRepository(BasketDbContext dbContext) : base(dbContext)
        { }

        public Task<Basket> GetByIdWithItemsAsync(int id)
        {
            return _dbContext.Baskets
                .Where(b => b.Id == id)
                .Include(o => o.Items)
                .FirstOrDefaultAsync();
        }

         
    }

}
