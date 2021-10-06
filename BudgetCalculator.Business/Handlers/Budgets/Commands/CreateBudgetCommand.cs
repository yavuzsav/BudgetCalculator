using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BudgetCalculator.Business.Constants;
using BudgetCalculator.Core.Utilities.Results;
using BudgetCalculator.DataAccess.Interfaces;
using BudgetCalculator.Entities.Concrete;
using MediatR;

namespace BudgetCalculator.Business.Handlers.Budgets.Commands
{
    public class CreateBudgetCommand : IRequest<IResult>
    {
        public Guid CategoryId { get; set; }
        public int Year { get; set; }
        public int Period { get; set; }
        public decimal Target { get; set; }
        public decimal Actual { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public class CreateBudgetCommandHandler : IRequestHandler<CreateBudgetCommand, IResult>
        {
            private readonly IBudgetRepository _budgetRepository;
            private readonly ICategoryRepository _categoryRepository;

            public CreateBudgetCommandHandler(IBudgetRepository budgetRepository,
                ICategoryRepository categoryRepository)
            {
                _budgetRepository = budgetRepository;
                _categoryRepository = categoryRepository;
            }

            public async Task<IResult> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
            {
                var category = await _categoryRepository.GetAsync(x => x.Id == request.CategoryId);
                if (category == null)
                    return new ErrorResult(Messages.CategoryDoesNotExist);

                var isThereBudgetRecord = _budgetRepository.Query().Any(x => x.Name == request.Name);
                if (isThereBudgetRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var budget = new Budget()
                {
                    Id = new Guid(),
                    CategoryId = request.CategoryId,
                    Year = request.Year,
                    Period = request.Period,
                    Target = request.Target,
                    Actual = request.Actual,
                    Name = request.Name,
                    Description = request.Description
                };

                _budgetRepository.Add(budget);
                await _budgetRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}