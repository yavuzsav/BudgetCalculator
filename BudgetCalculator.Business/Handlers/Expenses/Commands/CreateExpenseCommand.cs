using System;
using System.Threading;
using System.Threading.Tasks;
using BudgetCalculator.Business.Constants;
using BudgetCalculator.Core.Utilities.Results;
using BudgetCalculator.DataAccess.Interfaces;
using BudgetCalculator.Entities.Concrete;
using MediatR;

namespace BudgetCalculator.Business.Handlers.Expenses.Commands
{
    public class CreateExpenseCommand : IRequest<IResult>
    {
        public Guid BudgetId { get; set; }
        public decimal Planned { get; set; }
        public string Description { get; set; }

        public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, IResult>
        {
            private readonly IExpenseRepository _expenseRepository;
            private readonly IBudgetRepository _budgetRepository;

            public CreateExpenseCommandHandler(IExpenseRepository expenseRepository, IBudgetRepository budgetRepository)
            {
                _expenseRepository = expenseRepository;
                _budgetRepository = budgetRepository;
            }

            public async Task<IResult> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
            {
                var budget = await _budgetRepository.GetAsync(x => x.Id == request.BudgetId);
                if (budget == null)
                    return new ErrorResult(Messages.BudgetDoesNotExits);

                var expense = new Expense
                {
                    Id = new Guid(),
                    Planned = request.Planned,
                    Description = request.Description,
                    BudgetId = request.BudgetId,
                };

                _expenseRepository.Add(expense);
                await _expenseRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}