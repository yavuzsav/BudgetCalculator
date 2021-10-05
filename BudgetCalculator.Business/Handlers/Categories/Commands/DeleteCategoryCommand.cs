using System;
using System.Threading;
using System.Threading.Tasks;
using BudgetCalculator.Business.Constants;
using BudgetCalculator.Core.Utilities.Results;
using BudgetCalculator.DataAccess.Interfaces;
using MediatR;

namespace BudgetCalculator.Business.Handlers.Categories.Commands
{
    public class DeleteCategoryCommand : IRequest<IResult>
    {
        public Guid Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, IResult>
        {
            private readonly ICategoryRepository _categoryRepository;

            public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
            {
                _categoryRepository = categoryRepository;
            }

            public async Task<IResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = _categoryRepository.Get(x => x.Id == request.Id);
                if (category == null)
                    return new ErrorResult(Messages.NotExist);

                _categoryRepository.Delete(category);
                await _categoryRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}