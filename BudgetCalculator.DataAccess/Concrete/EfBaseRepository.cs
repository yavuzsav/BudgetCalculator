using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BudgetCalculator.DataAccess.Interfaces;
using BudgetCalculator.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BudgetCalculator.DataAccess.Concrete
{
    public class EfBaseRepository<TEntity, TContext> : IBaseRepository<TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext
    {
        protected readonly TContext Context;

        public EfBaseRepository(TContext context)
        {
            Context = context;
        }

        public TEntity Add(TEntity entity)
        {
            return Context.Add(entity).Entity;
        }

        public TEntity Update(TEntity entity)
        {
            return Context.Update(entity).Entity;

            // Context.Update(entity);
            // return entity;
        }

        public void Delete(TEntity entity)
        {
            Context.Remove(entity);
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null
                ? Context.Set<TEntity>().AsNoTracking()
                : Context.Set<TEntity>().Where(expression).AsNoTracking();
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null
                ? await Context.Set<TEntity>().ToListAsync()
                : await Context.Set<TEntity>().Where(expression).ToListAsync();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> expression)
        {
            return Context.Set<TEntity>().FirstOrDefault(expression);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Context.Set<TEntity>().AsQueryable().FirstOrDefaultAsync(expression);
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }

        public IQueryable<TEntity> Query()
        {
            return Context.Set<TEntity>();
        }

        public Task<int> ExecuteAsync(FormattableString interpolatedQueryString)
        {
            return Context.Database.ExecuteSqlInterpolatedAsync(interpolatedQueryString);
        }

        public int GetCount(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null
                ? Context.Set<TEntity>().Count()
                : Context.Set<TEntity>().Count(expression);
        }

        public Task<int> GetCountAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null
                ? Context.Set<TEntity>().CountAsync()
                : Context.Set<TEntity>().CountAsync(expression);
        }

        public TResult InTransaction<TResult>(Func<TResult> action, Action successAction = null,
            Action<Exception> exceptionAction = null)
        {
            var result = default(TResult);
            try
            {
                if (Context.Database.ProviderName.EndsWith("InMemory"))
                {
                    result = action();
                    SaveChanges();
                }
                else
                {
                    using (var tx = Context.Database.BeginTransaction())
                    {
                        try
                        {
                            result = action();
                            SaveChanges();
                            tx.Commit();
                        }
                        catch (Exception)
                        {
                            tx.Rollback();
                            throw;
                        }
                    }
                }

                successAction?.Invoke();
            }
            catch (Exception e)
            {
                if (exceptionAction == null)
                {
                    throw;
                }

                exceptionAction(e);
            }

            return result;
        }
    }
}