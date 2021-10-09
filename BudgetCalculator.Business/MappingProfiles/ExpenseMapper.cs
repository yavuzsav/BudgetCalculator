using AutoMapper;
using BudgetCalculator.Entities.Concrete;
using BudgetCalculator.Entities.Dtos;

namespace BudgetCalculator.Business.MappingProfiles
{
    public class ExpenseMapper : Profile
    {
        public ExpenseMapper()
        {
            CreateMap<Expense, ExpenseDto>().ForMember(dest => dest.BudgetName, opt => opt.MapFrom(x => x.Budget.Name));

            CreateMap<Expense, ExpenseDetailDto>()
                .ForMember(dest => dest.BudgetName, opt => opt.MapFrom(x => x.Budget.Name));
        }
    }
}