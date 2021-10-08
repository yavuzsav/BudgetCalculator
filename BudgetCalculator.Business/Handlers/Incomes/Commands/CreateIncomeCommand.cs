using System;
using System.Threading;
using System.Threading.Tasks;
using BudgetCalculator.Business.Constants;
using BudgetCalculator.Core.Utilities.Results;
using BudgetCalculator.DataAccess.Interfaces;
using BudgetCalculator.Entities.Concrete;
using MediatR;

namespace BudgetCalculator.Business.Handlers.Incomes.Commands
{
    public class CreateIncomeCommand : IRequest<IResult>
    {
        public Guid BudgetId { get; set; }
        public decimal Planned { get; set; }
        public string Description { get; set; }

        public class CreateIncomeCommandHandler : IRequestHandler<CreateIncomeCommand, IResult>
        {
            private readonly IIncomeRepository _incomeRepository;
            private readonly IBudgetRepository _budgetRepository;

            public CreateIncomeCommandHandler(IIncomeRepository incomeRepository, IBudgetRepository budgetRepository)
            {
                _incomeRepository = incomeRepository;
                _budgetRepository = budgetRepository;
            }

            public async Task<IResult> Handle(CreateIncomeCommand request, CancellationToken cancellationToken)
            {
                var budget = await _budgetRepository.GetAsync(x => x.Id == request.BudgetId);
                if (budget == null)
                    return new ErrorResult(Messages.BudgetDoesNotExits);

                var income = new Income()
                {
                    Id = new Guid(),
                    BudgetId = request.BudgetId,
                    Planned = request.Planned,
                    Description = request.Description
                };

                _incomeRepository.Add(income);
                await _incomeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}