using System;
using System.Threading;
using System.Threading.Tasks;
using BudgetCalculator.Business.Constants;
using BudgetCalculator.Core.Utilities.Results;
using BudgetCalculator.DataAccess.Interfaces;
using MediatR;

namespace BudgetCalculator.Business.Handlers.Budgets.Commands
{
    public class EditBudgetCommand : IRequest<IResult>
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public int Year { get; set; }
        public int Period { get; set; }
        public decimal Target { get; set; }
        public decimal Actual { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public class EditBudgetCommandHandler : IRequestHandler<EditBudgetCommand, IResult>
        {
            private readonly IBudgetRepository _budgetRepository;
            private readonly ICategoryRepository _categoryRepository;

            public EditBudgetCommandHandler(IBudgetRepository budgetRepository, ICategoryRepository categoryRepository)
            {
                _budgetRepository = budgetRepository;
                _categoryRepository = categoryRepository;
            }

            public async Task<IResult> Handle(EditBudgetCommand request, CancellationToken cancellationToken)
            {
                var budget = await _budgetRepository.GetAsync(x => x.Id == request.Id);
                if (budget == null)
                    return new ErrorResult(Messages.NotExist);

                var category = await _categoryRepository.GetAsync(x => x.Id == request.CategoryId);
                if (category == null)
                    return new ErrorResult(Messages.CategoryDoesNotExist);

                budget.CategoryId = request.CategoryId;
                budget.Year = request.Year;
                budget.Period = request.Period;
                budget.Target = request.Target;
                budget.Actual = request.Actual;
                budget.Name = request.Name;
                budget.Description = request.Description;

                _budgetRepository.Update(budget);
                await _budgetRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}