using System;
using System.Threading;
using System.Threading.Tasks;
using BudgetCalculator.Business.Constants;
using BudgetCalculator.Core.Utilities.Results;
using BudgetCalculator.DataAccess.Interfaces;
using MediatR;

namespace BudgetCalculator.Business.Handlers.Budgets.Commands
{
    public class DeleteBudgetCommand : IRequest<IResult>
    {
        public Guid Id { get; set; }

        public class DeleteBudgetCommandHandler : IRequestHandler<DeleteBudgetCommand, IResult>
        {
            private readonly IBudgetRepository _budgetRepository;

            public DeleteBudgetCommandHandler(IBudgetRepository budgetRepository)
            {
                _budgetRepository = budgetRepository;
            }

            public async Task<IResult> Handle(DeleteBudgetCommand request, CancellationToken cancellationToken)
            {
                var budget = await _budgetRepository.GetAsync(x => x.Id == request.Id);
                if (budget == null)
                    return new ErrorResult(Messages.NotExist);

                _budgetRepository.Delete(budget);
                await _budgetRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}