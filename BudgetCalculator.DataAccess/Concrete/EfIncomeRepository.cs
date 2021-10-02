using BudgetCalculator.DataAccess.Contexts;
using BudgetCalculator.DataAccess.Interfaces;
using BudgetCalculator.Entities.Concrete;

namespace BudgetCalculator.DataAccess.Concrete
{
    public class EfIncomeRepository : EfBaseRepository<Income, DataContext>, IIncomeRepository
    {
        public EfIncomeRepository(DataContext context) : base(context)
        {
        }
    }
}