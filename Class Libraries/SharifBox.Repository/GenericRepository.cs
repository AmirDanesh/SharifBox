using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SharifBox.Repository
{
    public interface IGenericRepository<C, E> where E : class where C : DbContext
    {
        E Add(E e);

        Task<E> AddAsync(E e);

        E Get(object id);

        Task<E> GetAsync(object id);

        Task<E> FindAsync(Expression<Func<E, bool>> match);

        ICollection<E> FindAll(Expression<Func<E, bool>> match);

        Task<ICollection<E>> FindAllAsync(Expression<Func<E, bool>> match);

        IQueryable<E> FindBy(Expression<Func<E, bool>> predicate);

        Task<ICollection<E>> FindByAsync(Expression<Func<E, bool>> predicate);

        IQueryable<E> GetAll();

        Task<ICollection<E>> GetAllAsync();

        IQueryable<E> GetAllIncluding(params Expression<Func<E, object>>[] includeProperties);

        E Update(E t, object key);

        Task<E> UpdateAsync(E t, object key);

        void Delete(E entity);
    }

    public class GenericRepository<C, E> : IGenericRepository<C, E> where E : class where C : DbContext
    {
        protected C Context { get; }

        public GenericRepository(C dbContext)
        {
            Context = dbContext;
        }

        public virtual E Add(E e)
        {
            Context.Set<E>().Add(e);

            return e;
        }

        public virtual async Task<E> AddAsync(E e)
        {
            await Context.Set<E>().AddAsync(e);

            return e;
        }

        public virtual E Get(object id)
        {
            return Context.Set<E>().Find(id);
        }

        public IQueryable<E> GetAll()
        {
            return Context.Set<E>();
        }

        public virtual async Task<E> GetAsync(object id)
        {
            return await Context.Set<E>().FindAsync(id);
        }

        public virtual E Find(Expression<Func<E, bool>> match)
        {
            return Context.Set<E>().SingleOrDefault(match);
        }

        public virtual async Task<E> FindAsync(Expression<Func<E, bool>> match)
        {
            return await Context.Set<E>().SingleOrDefaultAsync(match);
        }

        public ICollection<E> FindAll(Expression<Func<E, bool>> match)
        {
            return Context.Set<E>().Where(match).ToList();
        }

        public async Task<ICollection<E>> FindAllAsync(Expression<Func<E, bool>> match)
        {
            return await Context.Set<E>().Where(match).ToListAsync();
        }

        public virtual void Delete(E entity)
        {
            Context.Set<E>().Remove(entity);

            //_dbContext.SaveChanges();
        }

        //public virtual async Task<int> DeleteAsyn(E entity)
        //{
        //    _dbContext.Set<E>().Remove(entity);
        //    return await _dbContext.SaveChangesAsync();
        //}

        public virtual E Update(E e, object key)
        {
            if (e == null)
                return null;
            E exist = Context.Set<E>().Find(key);
            if (exist != null)
            {
                Context.Entry(exist).CurrentValues.SetValues(e);

                //_dbContext.SaveChanges();
            }
            return exist;
        }

        public virtual async Task<E> UpdateAsync(E e, object key)
        {
            if (e == null)
                return null;
            E exist = await Context.Set<E>().FindAsync(key);
            if (exist != null)
            {
                Context.Entry(exist).CurrentValues.SetValues(e);

                //await _dbContext.SaveChangesAsync();
            }
            return exist;
        }

        public int Count()
        {
            return Context.Set<E>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await Context.Set<E>().CountAsync();
        }

        public virtual IQueryable<E> FindBy(Expression<Func<E, bool>> predicate)
        {
            IQueryable<E> query = Context.Set<E>().Where(predicate);
            return query;
        }

        public virtual async Task<ICollection<E>> FindByAsync(Expression<Func<E, bool>> predicate)
        {
            return await Context.Set<E>().Where(predicate).ToListAsync();
        }

        public IQueryable<E> GetAllIncluding(params Expression<Func<E, object>>[] includeProperties)
        {
            IQueryable<E> queryable = GetAll();
            foreach (Expression<Func<E, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include<E, object>(includeProperty);
            }

            return queryable;
        }

        public virtual async Task<ICollection<E>> GetAllAsync()
        {
            return await Context.Set<E>().ToListAsync();
        }
    }
}