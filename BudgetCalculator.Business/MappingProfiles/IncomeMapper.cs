using AutoMapper;
using BudgetCalculator.Entities.Concrete;
using BudgetCalculator.Entities.Dtos;

namespace BudgetCalculator.Business.MappingProfiles
{
    public class IncomeMapper : Profile
    {
        public IncomeMapper()
        {
            CreateMap<Income, IncomeDto>().ForMember(dest => dest.BudgetName, opt => opt.MapFrom(x => x.Budget.Name));

            CreateMap<Income, IncomeDetailDto>()
                .ForMember(dest => dest.BudgetName, opt => opt.MapFrom(x => x.Budget.Name));
        }
    }
}