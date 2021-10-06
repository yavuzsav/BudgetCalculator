using System;
using System.Collections.Generic;
using BudgetCalculator.Entities.Abstract;
using BudgetCalculator.Entities.Concrete;

namespace BudgetCalculator.Entities.Dtos
{
    public class BudgetDetailDto : IDto
    {
        public BudgetDetailDto()
        {
            Incomes = new List<IncomeDto>();
            Expenses = new List<ExpenseDto>();
        }
        
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Year { get; set; }
        public int Period { get; set; }
        public decimal Target { get; set; }
        public decimal Actual { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<IncomeDto> Incomes { get; set; }

        public IList<ExpenseDto> Expenses { get; set; }
    }
}