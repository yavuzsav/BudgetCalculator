using System;
using BudgetCalculator.Entities.Abstract;

namespace BudgetCalculator.Entities.Dtos
{
    public class CategoryDto : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}