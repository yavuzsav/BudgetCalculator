using System;
using BudgetCalculator.Entities.Abstract;

namespace BudgetCalculator.Entities.Dtos
{
    public class BudgetDto : IDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Year { get; set; }
        public int Period { get; set; }
        public decimal Target { get; set; }
        public decimal Actual { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}