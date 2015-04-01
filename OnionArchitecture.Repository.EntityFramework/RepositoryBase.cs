using OnionArchitecture.Core.Infrastructure.Repositories;
using OnionArchitecture.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace OnionArchitecture.Repository.EntityFramework
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : EntityBase
    {
        protected readonly IDbContext Context;
        protected DbSet<T> Set;

        protected RepositoryBase(IDbContext context)
        {
            Context = context;
            Set = Context.Set<T>() as DbSet<T>;
        }

        public T FindBy(int id, bool includeNavigationProperties = false)
        {
            Expression<Func<T, bool>> filter = (e => e.Id == id);
            return FindBy(filter, includeNavigationProperties).FirstOrDefault();
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> filter, bool includeNavigationProperties = false)
        {
            if (includeNavigationProperties)
            {
                return Set.IncludeNavigationProperties().Where(filter).ToList();
            }
            else
            {
                return Set.Where(filter).ToList();
            }
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

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> filter, PaginationInfo paginationInfo, bool includeNavigationProperties = false)
        {
            IQueryable<T> query = includeNavigationProperties ? Set.IncludeNavigationProperties() : Set;

            return FindWithEagerLoadedProperties(filter, paginationInfo, query);
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

        public IEnumerable<T> FindAll(bool includeNavigationProperties = false)
        {
            if (includeNavigationProperties)
            {
                return Set.IncludeNavigationProperties().ToList();
            }
            else
            {
                return GetQueryable().ToList();
            }
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

            DbEntityEntry dbEntityEntry = Context.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                Set.Add(entity);
            }
        }

        public void Delete(int id)
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
            DbEntityEntry dbEntityEntry = Context.Entry(entity);
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
            DbEntityEntry dbEntityEntry = Context.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                Set.Attach(entity);
            }

            dbEntityEntry.State = EntityState.Modified;
        }
    }
}
