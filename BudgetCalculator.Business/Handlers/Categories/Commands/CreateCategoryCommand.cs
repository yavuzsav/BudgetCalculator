using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BudgetCalculator.Business.Constants;
using BudgetCalculator.Core.Utilities.Results;
using BudgetCalculator.DataAccess.Interfaces;
using BudgetCalculator.Entities.Concrete;
using MediatR;

namespace BudgetCalculator.Business.Handlers.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<IResult>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, IResult>
        {
            private readonly ICategoryRepository _categoryRepository;

            public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
            {
                _categoryRepository = categoryRepository;
            }

            public async Task<IResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                var isThereCategoryRecord = _categoryRepository.Query().Any(x => x.Name == request.Name);

                if (isThereCategoryRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var category = new Category()
                {
                    Id = new Guid(),
                    Name = request.Name,
                    Description = request.Description
                };

                _categoryRepository.Add(category);
                await _categoryRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}