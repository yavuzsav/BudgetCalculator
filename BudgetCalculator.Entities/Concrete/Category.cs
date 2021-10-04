using System;
using System.Collections.Generic;
using BudgetCalculator.Entities.Abstract;

namespace BudgetCalculator.Entities.Concrete
{
    public class Category : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Budget> Budgets { get; set; }
    }
}