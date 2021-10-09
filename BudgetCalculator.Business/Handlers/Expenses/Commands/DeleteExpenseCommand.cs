using System;
using System.Threading;
using System.Threading.Tasks;
using BudgetCalculator.Business.Constants;
using BudgetCalculator.Core.Utilities.Results;
using BudgetCalculator.DataAccess.Interfaces;
using MediatR;

namespace BudgetCalculator.Business.Handlers.Expenses.Commands
{
    public class DeleteExpenseCommand : IRequest<IResult>
    {
        public Guid Id { get; set; }

        public class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand, IResult>
        {
            private readonly IExpenseRepository _expenseRepository;

            public DeleteExpenseCommandHandler(IExpenseRepository expenseRepository)
            {
                _expenseRepository = expenseRepository;
            }

            public async Task<IResult> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
            {
                var expense = await _expenseRepository.GetAsync(x => x.Id == request.Id);
                if (expense == null)
                    return new ErrorResult(Messages.ExpenseDoesNotExits);

                _expenseRepository.Delete(expense);
                await _expenseRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}