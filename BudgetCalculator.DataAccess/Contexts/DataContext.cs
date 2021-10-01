using BudgetCalculator.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BudgetCalculator.DataAccess.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Budget>().HasMany(x => x.Expenses).WithOne(x => x.Budget).HasForeignKey(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Budget>().HasMany(x => x.Incomes).WithOne(x => x.Budget).HasForeignKey(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}