using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using BasketApi.Infrastructure.Entities;

namespace BasketApi.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        T GetSingleBySpec(ISpecification<T> spec);
        T GetSingle(Expression<Func<T, bool>> criteria);
        IEnumerable<T> ListAll();
        IEnumerable<T> List(ISpecification<T> spec);
        IEnumerable<T> List(Expression<Func<T, bool>> criteria);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        int GetNewId(T entity);
    }   
}
