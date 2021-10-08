using System;
using BudgetCalculator.Entities.Abstract;

namespace BudgetCalculator.Entities.Dtos
{
    public class IncomeDetailDto : IDto
    {
        public Guid Id { get; set; }
        public Guid BudgetId { get; set; }
        public string BudgetName { get; set; }
        public decimal Planned { get; set; }
        public decimal Actual { get; set; }
        public string Description { get; set; }
    }
}