using OnionArchitecture.Core.Infrastructure.Repositories;
using OnionArchitecture.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace OnionArchitecture.Repository.EntityFramework
{
    public abstract class RepositoryBase<T, K> : IRepository<T, K> 
        where T : EntityBase<K>
        where K : IEquatable<K>
    {
        protected readonly IDbContext Context;
        protected IDbSet<T> Set;

        protected RepositoryBase(IDbContext context)
        {
            Context = context;
            Set = Context.Set<T>();
        }

        public T FindBy(K id, params Expression<Func<T, object>>[] includeExpressions)
        {
            Expression<Func<T, bool>> filter = (e => e.Id.Equals(id));
            return FindBy(filter, includeExpressions).FirstOrDefault();
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> query = Set;

            foreach (var includeExpression in includeExpressions)
            {
                query = query.Include(includeExpression);
            }

            return query.Where(filter).ToList();
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> filter, PaginationInfo paginationInfo, params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> query = Set;

            foreach (var includeExpression in includeExpressions)
            {
                query = query.Include(includeExpression);
            }

            return FindWithEagerLoadedProperties(filter, paginationInfo, query);
        }

        private IEnumerable<T> FindWithEagerLoadedProperties(Expression<Func<T, bool>> filter, PaginationInfo paginationInfo, IQueryable<T> query)
        {
            if (filter != null)
            {
                query = query.Where(filter);
            }

            paginationInfo.TotalItems = query.Count();

            //Convert from 1-based to 0-based
            int currentPage = paginationInfo.CurrentPage - 1;

            query = query.OrderByDescending(e => e.Id)
                         .Skip(currentPage * paginationInfo.PageSize)
                         .Take(paginationInfo.PageSize);

            return query.ToList();
        }

        public IEnumerable<T> FindAll(params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> query = Set;

            foreach (var includeExpression in includeExpressions)
            {
                query = query.Include(includeExpression);
            }

            return query.ToList();
        }

        private IQueryable<T> GetQueryable()
        {
            return Set;
        }

        public void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("Entity cannot be null");
            }

            var dbEntityEntry = Context.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                Set.Add(entity);
            }
        }

        public void Delete(K id)
        {
            T entity = FindBy(id);
            if (entity == null)
            {
                throw new ArgumentException("Cannot find entity with id " + id);
            }

            Delete(entity);
        }

        public void Delete(T entity)
        {
            var dbEntityEntry = Context.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                Set.Attach(entity);
                Set.Remove(entity);
            }
        }

        public void Update(T entity)
        {
            var dbEntityEntry = Context.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                Set.Attach(entity);
            }

            dbEntityEntry.State = EntityState.Modified;
        }
    }
}
