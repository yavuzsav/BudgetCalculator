using BudgetCalculator.DataAccess.Contexts;
using BudgetCalculator.DataAccess.Interfaces;
using BudgetCalculator.Entities.Concrete;

namespace BudgetCalculator.DataAccess.Concrete
{
    public class EfBudgetRepository : EfBaseRepository<Budget, DataContext>, IBudgetRepository
    {
        public EfBudgetRepository(DataContext context) : base(context)
        {
        }
    }
}