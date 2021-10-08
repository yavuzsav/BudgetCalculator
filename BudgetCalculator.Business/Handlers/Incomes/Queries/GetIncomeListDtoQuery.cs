using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BudgetCalculator.Core.Utilities.Results;
using BudgetCalculator.DataAccess.Interfaces;
using BudgetCalculator.Entities.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BudgetCalculator.Business.Handlers.Incomes.Queries
{
    public class GetIncomeListDtoQuery : IRequest<IDataResult<IEnumerable<IncomeDto>>>
    {
        public class
            GetIncomeListDtoQueryHandler : IRequestHandler<GetIncomeListDtoQuery, IDataResult<IEnumerable<IncomeDto>>>
        {
            private readonly IIncomeRepository _incomeRepository;
            private readonly IMapper _mapper;

            public GetIncomeListDtoQueryHandler(IIncomeRepository incomeRepository, IMapper mapper)
            {
                _incomeRepository = incomeRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<IEnumerable<IncomeDto>>> Handle(GetIncomeListDtoQuery request,
                CancellationToken cancellationToken)
            {
                var incomes = await _incomeRepository.Query().Include(x => x.Budget)
                    .ToListAsync(cancellationToken: cancellationToken);
                var mappedData = _mapper.Map<IEnumerable<IncomeDto>>(incomes);
                return new SuccessDataResult<IEnumerable<IncomeDto>>(mappedData);
            }
        }
    }
}