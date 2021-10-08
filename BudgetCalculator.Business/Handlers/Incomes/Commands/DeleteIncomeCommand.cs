using System;
using System.Threading;
using System.Threading.Tasks;
using BudgetCalculator.Business.Constants;
using BudgetCalculator.Core.Utilities.Results;
using BudgetCalculator.DataAccess.Interfaces;
using MediatR;

namespace BudgetCalculator.Business.Handlers.Incomes.Commands
{
    public class DeleteIncomeCommand : IRequest<IResult>
    {
        public Guid Id { get; set; }

        public class DeleteIncomeCommandHandler : IRequestHandler<DeleteIncomeCommand, IResult>
        {
            private readonly IIncomeRepository _incomeRepository;

            public DeleteIncomeCommandHandler(IIncomeRepository incomeRepository)
            {
                _incomeRepository = incomeRepository;
            }

            public async Task<IResult> Handle(DeleteIncomeCommand request, CancellationToken cancellationToken)
            {
                var income = await _incomeRepository.GetAsync(x => x.Id == request.Id);
                if (income == null)
                    return new ErrorResult(Messages.IncomeDoesNotExist);

                _incomeRepository.Delete(income);
                await _incomeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}