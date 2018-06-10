using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BasketApi.Infrastructure.Entities;

namespace BasketApi.Infrastructure.Interfaces
{
    /// <summary>
    /// Interface for the access to the database entities
    /// </summary>
    /// <typeparam name="T">Database entity type</typeparam>
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> ListAllAsync();
        Task<List<T>> ListAsync(Expression<Func<T, bool>> criteria);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
