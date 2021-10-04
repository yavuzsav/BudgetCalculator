using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BudgetCalculator.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BudgetCalculator.Entities.Concrete
{
    [Index(nameof(Id))]
    public class Category : IEntity
    {
        [Key] public Guid Id { get; set; }
        [MaxLength(120)] public string Name { get; set; }
        [MaxLength(500)] public string Description { get; set; }

        public ICollection<Budget> Budgets { get; set; }
    }
}