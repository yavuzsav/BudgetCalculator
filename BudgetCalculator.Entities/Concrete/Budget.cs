using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetCalculator.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BudgetCalculator.Entities.Concrete
{
    [Index(nameof(Id))]
    public class Budget : IEntity
    {
        [Key] public Guid Id { get; set; }
        [Range(2021, 2100)] public int Year { get; set; }
        [Range(1, 12)] public int Period { get; set; }
        [Column(TypeName = "decimal(12,2)")] public decimal Target { get; set; }
        [MaxLength(500)] public string Description { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Income> Incomes { get; set; }
        public ICollection<Expense> Expenses { get; set; }
    }
}