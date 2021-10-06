using BudgetCalculator.Business.Handlers.Budgets.Commands;
using FluentValidation;

namespace BudgetCalculator.Business.Handlers.Budgets.ValidationRules
{
    public class CreateBudgetValidator : AbstractValidator<CreateBudgetCommand>
    {
        public CreateBudgetValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Year).NotEmpty().ExclusiveBetween(1, 12);
            RuleFor(x => x.Period).NotEmpty().ExclusiveBetween(1, 12);
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleFor(x => x.Target).NotEmpty().ScalePrecision(12, 2);
        }
    }

    public class EditBudgetValidator : AbstractValidator<EditBudgetCommand>
    {
        public EditBudgetValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Year).NotEmpty().ExclusiveBetween(1, 12);
            RuleFor(x => x.Period).NotEmpty().ExclusiveBetween(1, 12);
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleFor(x => x.Target).NotEmpty().ScalePrecision(12, 2);
        }
    }

    public class DeleteBudgetValidator : AbstractValidator<DeleteBudgetCommand>
    {
        public DeleteBudgetValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}