using AutoMapper;
using BudgetCalculator.Entities.Concrete;
using BudgetCalculator.Entities.Dtos;

namespace BudgetCalculator.Business.MappingProfiles
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<Category, CategoryDto>();
        }
    }
}