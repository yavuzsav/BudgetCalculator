using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BudgetCalculator.Business.Constants;
using BudgetCalculator.Core.Utilities.Results;
using BudgetCalculator.DataAccess.Interfaces;
using MediatR;

namespace BudgetCalculator.Business.Handlers.Categories.Commands
{
    public class EditCategoryCommand : IRequest<IResult>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public class EditCategoryCommandHandler : IRequestHandler<EditCategoryCommand, IResult>
        {
            private readonly ICategoryRepository _categoryRepository;

            public EditCategoryCommandHandler(ICategoryRepository categoryRepository)
            {
                _categoryRepository = categoryRepository;
            }

            public async Task<IResult> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = await _categoryRepository.GetAsync(x => x.Id == request.Id);

                if (category == null)
                    return new ErrorResult(Messages.NotExist);

                var isThereCategoryRecord = _categoryRepository.Query().Any(x => x.Name == request.Name);

                if (isThereCategoryRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                category.Name = request.Name;
                category.Description = request.Description;
                _categoryRepository.Update(category);
                await _categoryRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}