using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BudgetCalculator.Core.Utilities.Results;
using BudgetCalculator.DataAccess.Interfaces;
using BudgetCalculator.Entities.Dtos;
using MediatR;

namespace BudgetCalculator.Business.Handlers.Categories.Queries
{
    public class GetCategoryListDtoQuery : IRequest<IDataResult<IEnumerable<CategoryDto>>>
    {
        public class
            GetCategoryListDtoQueryHandler : IRequestHandler<GetCategoryListDtoQuery,
                IDataResult<IEnumerable<CategoryDto>>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IMapper _mapper;

            public GetCategoryListDtoQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
            {
                _categoryRepository = categoryRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<IEnumerable<CategoryDto>>> Handle(GetCategoryListDtoQuery request,
                CancellationToken cancellationToken)
            {
                var category = await _categoryRepository.GetListAsync();
                var mappedData = _mapper.Map<List<CategoryDto>>(category);
                return new SuccessDataResult<IEnumerable<CategoryDto>>(mappedData);
            }
        }
    }
}