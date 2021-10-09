using BudgetCalculator.Business.Handlers.Expenses.Commands;
using FluentValidation;

namespace BudgetCalculator.Business.Handlers.Expenses.ValidationRules
{
    public class CreateExpenseValidator : AbstractValidator<CreateExpenseCommand>
    {
        public CreateExpenseValidator()
        {
            RuleFor(x => x.BudgetId).NotEmpty();
            RuleFor(x => x.Planned).NotEmpty().ScalePrecision(12, 2);
        }
    }

    public class EditExpenseValidator : AbstractValidator<EditExpenseCommand>
    {
        public EditExpenseValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.BudgetId).NotEmpty();
            RuleFor(x => x.Planned).NotEmpty().ScalePrecision(12, 2);
        }
    }

    public class DeleteExpenseValidator : AbstractValidator<DeleteExpenseCommand>
    {
        public DeleteExpenseValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}