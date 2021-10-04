using BudgetCalculator.DataAccess.Contexts;
using BudgetCalculator.DataAccess.Interfaces;
using BudgetCalculator.Entities.Concrete;

namespace BudgetCalculator.DataAccess.Concrete
{
    public class EfCategoryRepository : EfBaseRepository<Category, DataContext>, ICategoryRepository
    {
        public EfCategoryRepository(DataContext context) : base(context)
        {
        }
    }
}