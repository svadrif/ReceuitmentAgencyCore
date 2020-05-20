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
        private AppDbContext Ctx { get; }
        public GenericRepository(AppDbContext ctx)
        {
            Ctx = ctx;
        }

        public IQueryable<T> GetAll()
        {
            return Ctx.Set<T>();
        }
        public virtual async Task<ICollection<T>> GetAllAsyn()
        {
            return await Ctx.Set<T>().ToListAsync();
        }
        public virtual T Get(int id)
        {
            return Ctx.Set<T>().Find(id);
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await Ctx.Set<T>().FindAsync(id);
        }

        public virtual T Add(T t)
        {
            Ctx.Set<T>().Add(t);
            Ctx.SaveChanges();
            return t;
        }
        public virtual ICollection<T> AddRange(ICollection<T> t)
        {
            Ctx.Set<T>().AddRange(t);
            Ctx.SaveChanges();
            return t;
        }

        public virtual async Task<T> AddAsyn(T t)
        {
            Ctx.Set<T>().Add(t);
            await Ctx.SaveChangesAsync();
            return t;

        }

        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return Ctx.Set<T>().SingleOrDefault(match);
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await Ctx.Set<T>().SingleOrDefaultAsync(match);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return Ctx.Set<T>().Where(match).ToList();
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await Ctx.Set<T>().Where(match).ToListAsync();
        }

        public virtual void Delete(T entity)
        {
            Ctx.Set<T>().Remove(entity);
            Ctx.SaveChanges();
        }

        public virtual void DeleteRange(List<T> entity)
        {
            Ctx.Set<T>().RemoveRange(entity);
            Ctx.SaveChanges();
        }

        public virtual async Task<int> DeleteAsyn(T entity)
        {
            Ctx.Set<T>().Remove(entity);
            return await Ctx.SaveChangesAsync();
        }

        public virtual T Update(T t, object key)
        {
            if (t == null)
                return null;
            T exist = Ctx.Set<T>().Find(key);
            if (exist != null)
            {
                Ctx.Entry(exist).CurrentValues.SetValues(t);
                Ctx.SaveChanges();
            }
            return exist;
        }

        public virtual async Task<T> UpdateAsyn(T t, object key)
        {
            if (t == null)
                return null;
            T exist = await Ctx.Set<T>().FindAsync(key);
            if (exist != null)
            {
                Ctx.Entry(exist).CurrentValues.SetValues(t);
                await Ctx.SaveChangesAsync();
            }
            return exist;
        }

        public int Count()
        {
            return Ctx.Set<T>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await Ctx.Set<T>().CountAsync();
        }

        public virtual void Save()
        {
            Ctx.SaveChanges();
        }

        public async virtual Task<int> SaveAsync()
        {
            return await Ctx.SaveChangesAsync();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = Ctx.Set<T>().Where(predicate);
            return query;
        }

        public virtual IQueryable<T> FindAllBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = Ctx.Set<T>().Where(predicate);
            return query;
        }

        public virtual async Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate)
        {
            return await Ctx.Set<T>().Where(predicate).ToListAsync();
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
                    Ctx.Dispose();
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
