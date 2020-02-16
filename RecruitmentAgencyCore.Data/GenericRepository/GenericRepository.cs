using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentAgencyCore.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public AppDbContext _ctx { get; }
        public GenericRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public IQueryable<T> GetAll()
        {
            return _ctx.Set<T>();
        }
        public virtual async Task<ICollection<T>> GetAllAsyn()
        {
            return await _ctx.Set<T>().ToListAsync();
        }
        public virtual T Get(int id)
        {
            return _ctx.Set<T>().Find(id);
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await _ctx.Set<T>().FindAsync(id);
        }

        public virtual T Add(T t)
        {
            _ctx.Set<T>().Add(t);
            _ctx.SaveChanges();
            return t;
        }
        public virtual ICollection<T> AddRange(ICollection<T> t)
        {
            _ctx.Set<T>().AddRange(t);
            _ctx.SaveChanges();
            return t;
        }

        public virtual async Task<T> AddAsyn(T t)
        {
            _ctx.Set<T>().Add(t);
            await _ctx.SaveChangesAsync();
            return t;

        }

        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return _ctx.Set<T>().SingleOrDefault(match);
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _ctx.Set<T>().SingleOrDefaultAsync(match);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return _ctx.Set<T>().Where(match).ToList();
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await _ctx.Set<T>().Where(match).ToListAsync();
        }

        public virtual void Delete(T entity)
        {
            _ctx.Set<T>().Remove(entity);
            _ctx.SaveChanges();
        }

        public virtual void DeleteRange(List<T> entity)
        {
            _ctx.Set<T>().RemoveRange(entity);
            _ctx.SaveChanges();
        }

        public virtual async Task<int> DeleteAsyn(T entity)
        {
            _ctx.Set<T>().Remove(entity);
            return await _ctx.SaveChangesAsync();
        }

        public virtual T Update(T t, object key)
        {
            if (t == null)
                return null;
            T exist = _ctx.Set<T>().Find(key);
            if (exist != null)
            {
                _ctx.Entry(exist).CurrentValues.SetValues(t);
                _ctx.SaveChanges();
            }
            return exist;
        }

        public virtual async Task<T> UpdateAsyn(T t, object key)
        {
            if (t == null)
                return null;
            T exist = await _ctx.Set<T>().FindAsync(key);
            if (exist != null)
            {
                _ctx.Entry(exist).CurrentValues.SetValues(t);
                await _ctx.SaveChangesAsync();
            }
            return exist;
        }

        public int Count()
        {
            return _ctx.Set<T>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await _ctx.Set<T>().CountAsync();
        }

        public virtual void Save()
        {

            _ctx.SaveChanges();
        }

        public async virtual Task<int> SaveAsync()
        {
            return await _ctx.SaveChangesAsync();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _ctx.Set<T>().Where(predicate);
            return query;
        }

        public virtual IQueryable<T> FindAllBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _ctx.Set<T>().Where(predicate);
            return query;
        }

        public virtual async Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate)
        {
            return await _ctx.Set<T>().Where(predicate).ToListAsync();
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include(includeProperty);
            }
            return queryable;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _ctx.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
