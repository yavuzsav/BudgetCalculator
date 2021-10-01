using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetCalculator.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BudgetCalculator.Entities.Concrete
{
    [Index(nameof(Id))]
    public class Income : IEntity
    {
        [Key] public Guid Id { get; set; }
        [Column(TypeName = "decimal(12,2)")] public decimal Planned { get; set; }
        [Column(TypeName = "decimal(12,2)")] public decimal Actual { get; set; }
        [MaxLength(500)] public string Description { get; set; }

        public Budget Budget { get; set; }
        public Guid BudgetId { get; set; }

        public Expense Expense { get; set; }
        public Guid ExpenseId { get; set; }
    }
}