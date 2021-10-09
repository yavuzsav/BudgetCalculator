using System;
using System.Threading;
using System.Threading.Tasks;
using BudgetCalculator.Business.Constants;
using BudgetCalculator.Core.Utilities.Results;
using BudgetCalculator.DataAccess.Interfaces;
using MediatR;

namespace BudgetCalculator.Business.Handlers.Expenses.Commands
{
    public class EditExpenseCommand : IRequest<IResult>
    {
        public Guid Id { get; set; }
        public Guid BudgetId { get; set; }
        public decimal Planned { get; set; }
        public string Description { get; set; }

        public class EditExpenseCommandHandler : IRequestHandler<EditExpenseCommand, IResult>
        {
            private readonly IExpenseRepository _expenseRepository;
            private readonly IBudgetRepository _budgetRepository;

            public EditExpenseCommandHandler(IExpenseRepository expenseRepository, IBudgetRepository budgetRepository)
            {
                _expenseRepository = expenseRepository;
                _budgetRepository = budgetRepository;
            }

            public async Task<IResult> Handle(EditExpenseCommand request, CancellationToken cancellationToken)
            {
                var expense = await _expenseRepository.GetAsync(x => x.Id == request.Id);
                if (expense == null)
                    return new ErrorResult(Messages.ExpenseDoesNotExits);

                var budget = await _budgetRepository.GetAsync(x => x.Id == request.BudgetId);
                if (budget == null)
                    return new ErrorResult(Messages.BudgetDoesNotExits);

                expense.BudgetId = request.BudgetId;
                expense.Planned = request.Planned;
                expense.Description = request.Description;

                _expenseRepository.Update(expense);
                await _expenseRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}