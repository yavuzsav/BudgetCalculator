using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BudgetCalculator.Core.Utilities.Results;
using BudgetCalculator.DataAccess.Interfaces;
using BudgetCalculator.Entities.Dtos;
using MediatR;

namespace BudgetCalculator.Business.Handlers.Categories.Queries
{
    public class GetCategoryDtoQuery : IRequest<IDataResult<CategoryDto>>
    {
        public Guid Id { get; set; }

        public class GetCategoryDtoQueryHandler : IRequestHandler<GetCategoryDtoQuery, IDataResult<CategoryDto>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IMapper _mapper;

            public GetCategoryDtoQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
            {
                _categoryRepository = categoryRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<CategoryDto>> Handle(GetCategoryDtoQuery request,
                CancellationToken cancellationToken)
            {
                var category = await _categoryRepository.GetAsync(x => x.Id == request.Id);
                var mappedData = _mapper.Map<CategoryDto>(category);
                return new SuccessDataResult<CategoryDto>(mappedData);
            }
        }
    }
}