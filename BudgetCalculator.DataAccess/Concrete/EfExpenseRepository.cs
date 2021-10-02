using BudgetCalculator.DataAccess.Contexts;
using BudgetCalculator.DataAccess.Interfaces;
using BudgetCalculator.Entities.Concrete;

namespace BudgetCalculator.DataAccess.Concrete
{
    public class EfExpenseRepository : EfBaseRepository<Expense, DataContext>, IExpenseRepository
    {
        public EfExpenseRepository(DataContext context) : base(context)
        {
        }
    }
}