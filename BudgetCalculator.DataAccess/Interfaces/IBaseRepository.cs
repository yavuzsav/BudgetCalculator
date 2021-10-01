using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BudgetCalculator.Entities.Abstract;

namespace BudgetCalculator.DataAccess.Interfaces
{
    public interface IBaseRepository<T> where T : class, IEntity
    {
        T Add(T entity);
        T Update(T entity);
        void Delete(T delete);
        IEnumerable<T> GetList(Expression<Func<T, bool>> expression = null);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> expression = null);
        T Get(Expression<Func<T, bool>> expression = null);
        Task<T> GetAsync(Expression<Func<T, bool>> expression = null);
        int SaveChanges();
        Task<int> SaveChangesAsync();
        IQueryable<T> Query();
        Task<int> Execute(FormattableString interpolatedQueryString);
        int GetCount(Expression<Func<T, bool>> expression = null);
        Task<int> GetCountAsync(Expression<Func<T, bool>> expression = null);
        TResult InTransaction<TResult>(Func<TResult> action, Action successAction = null,
            Action<Exception> exceptionAction = null);
    }
}