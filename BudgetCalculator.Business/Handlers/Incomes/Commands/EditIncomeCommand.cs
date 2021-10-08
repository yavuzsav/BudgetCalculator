using System;
using System.Threading;
using System.Threading.Tasks;
using BudgetCalculator.Business.Constants;
using BudgetCalculator.Core.Utilities.Results;
using BudgetCalculator.DataAccess.Interfaces;
using MediatR;

namespace BudgetCalculator.Business.Handlers.Incomes.Commands
{
    public class EditIncomeCommand : IRequest<IResult>
    {
        public Guid Id { get; set; }
        public Guid BudgetId { get; set; }
        public decimal Planned { get; set; }
        public string Description { get; set; }

        public class EditIncomeCommandHandler : IRequestHandler<EditIncomeCommand, IResult>
        {
            private readonly IIncomeRepository _incomeRepository;
            private readonly IBudgetRepository _budgetRepository;

            public EditIncomeCommandHandler(IIncomeRepository incomeRepository, IBudgetRepository budgetRepository)
            {
                _incomeRepository = incomeRepository;
                _budgetRepository = budgetRepository;
            }

            public async Task<IResult> Handle(EditIncomeCommand request, CancellationToken cancellationToken)
            {
                var income = await _incomeRepository.GetAsync(x => x.Id == request.Id);
                if (income == null)
                    return new ErrorResult(Messages.IncomeDoesNotExist);

                var budget = await _budgetRepository.GetAsync(x => x.Id == request.BudgetId);
                if (budget == null)
                    return new ErrorResult(Messages.BudgetDoesNotExits);

                income.BudgetId = request.BudgetId;
                income.Planned = request.Planned;
                income.Description = request.Description;

                _incomeRepository.Add(income);
                await _incomeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}