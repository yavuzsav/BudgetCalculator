using BudgetCalculator.Business.Handlers.Categories.Commands;
using BudgetCalculator.Business.Handlers.Categories.Queries;
using FluentValidation;

namespace BudgetCalculator.Business.Handlers.Categories.ValidationRules
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }

    public class EditCategoryValidator : AbstractValidator<EditCategoryCommand>
    {
        public EditCategoryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }

    public class GetCategoryDtoValidator : AbstractValidator<GetCategoryDtoQuery>
    {
        public GetCategoryDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}