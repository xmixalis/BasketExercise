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
    /// <summary>
    /// Class that performs the basic operations for a database entity
    /// </summary>
    /// <typeparam name="T">Type of the database entity</typeparam>
    public class BaseRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly BasketDbContext _dbContext;

        public BaseRepository(BasketDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Queries a specific record of a database entity
        /// </summary>
        /// <param name="id">ID of the entity required</param>
        /// <returns>The entity with the specific ID provided. 
        /// If not found then returns null</returns>
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Lists all records of a database entity
        /// </summary>
        /// <returns>List of entity instances for all the records</returns>
        public async Task<List<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Lists all records of a database entity for a given criteria
        /// </summary>
        /// <param name="criteria">Criteria to filter records</param>
        /// <returns>Filtered List of entity instances</returns>
        public async Task<List<T>> ListAsync(Expression<Func<T, bool>> criteria)
        {
            return await _dbContext.Set<T>().AsQueryable().Where(criteria).ToListAsync();
        }

        /// <summary>
        /// Adds a new record to the database
        /// </summary>
        /// <param name="entity">Entity instance of the record to be added</param>
        /// <returns>Entity instance of the added record</returns>
        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        /// <summary>
        /// Updates a record of the database
        /// </summary>
        /// <param name="entity">Entity instance of the record to be updated</param>
        /// <returns>A task that represents the asynchronous save operation</returns>
        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a record from the database
        /// </summary>
        /// <param name="entity">Entity instance of the record to be deleted</param>
        /// <returns>A task that represents the asynchronous save operation</returns>
        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }

}
