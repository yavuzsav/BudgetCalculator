using BudgetCalculator.Business.Handlers.Incomes.Commands;
using FluentValidation;

namespace BudgetCalculator.Business.Handlers.Incomes.ValidationRules
{
    public class CreateIncomeValidator : AbstractValidator<CreateIncomeCommand>
    {
        public CreateIncomeValidator()
        {
            RuleFor(x => x.BudgetId).NotEmpty();
            RuleFor(x => x.Planned).NotEmpty().ScalePrecision(12, 2);
        }
    }

    public class EditIncomeValidator : AbstractValidator<EditIncomeCommand>
    {
        public EditIncomeValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.BudgetId).NotEmpty();
            RuleFor(x => x.Planned).NotEmpty().ScalePrecision(12, 2);
        }
    }

    public class DeleteIncomeValidator : AbstractValidator<DeleteIncomeCommand>
    {
        public DeleteIncomeValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}