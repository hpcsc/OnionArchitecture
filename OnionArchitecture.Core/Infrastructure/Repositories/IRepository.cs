using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using OnionArchitecture.Core.Models;

namespace OnionArchitecture.Core.Infrastructure.Repositories
{
    public interface IRepository<T> where T : EntityBase
    {
        T FindBy(int id, bool includeNavigationProperties = false);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> filter, bool includeNavigationProperties = false);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeExpressions);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> filter, PaginationInfo paginationInfo, bool includeNavigationProperties = false);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> filter, PaginationInfo paginationInfo, params Expression<Func<T, object>>[] includeExpressions);
        IEnumerable<T> FindAll(bool includeNavigationProperties = false);
        IEnumerable<T> FindAll(params Expression<Func<T, object>>[] includeExpressions);
        void Add(T entity);
        void Delete(int id);
        void Delete(T entity);
        void Update(T entity);
    }
}
