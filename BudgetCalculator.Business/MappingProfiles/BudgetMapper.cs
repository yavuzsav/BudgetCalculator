using AutoMapper;
using BudgetCalculator.Entities.Concrete;
using BudgetCalculator.Entities.Dtos;

namespace BudgetCalculator.Business.MappingProfiles
{
    public class BudgetMapper : Profile
    {
        public BudgetMapper()
        {
            CreateMap<Budget, BudgetDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(x => x.Category.Name));

            CreateMap<Budget, BudgetDetailDto>()
                .ForMember(dest => dest.Incomes, opt => opt.MapFrom(x => x.Incomes))
                .ForMember(dest => dest.Expenses, opt => opt.MapFrom(x => x.Expenses));
        }
    }
}