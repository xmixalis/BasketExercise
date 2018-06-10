using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BasketApi.Infrastructure.Entities;
using BasketApi.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BasketApi.Infrastructure.Repos
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly BasketDbContext _dbContext;

        public BaseRepository(BasketDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public async Task<List<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
        public async Task<List<T>> ListAsync(Expression<Func<T, bool>> criteria)
        {
            return await _dbContext.Set<T>().AsQueryable().Where(criteria).ToListAsync();
        }
  
        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        private static readonly object padlock = new object();
        public int GetNewId(T entity)
        {
            lock (padlock)
            {
                int? maxid = ListAllAsync().Result
                             .OrderByDescending(b => b.Id)
                             .Take(1)
                             .Select(b => b.Id)
                             .FirstOrDefault();

                return (maxid ?? 0) + 1;
            }
        }
    }

}
