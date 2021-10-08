using System;
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
    public class GetIncomeDetailDtoQuery : IRequest<IDataResult<IncomeDetailDto>>
    {
        public Guid Id { get; set; }

        public class
            GetIncomeDetailDtoQueryHandler : IRequestHandler<GetIncomeDetailDtoQuery, IDataResult<IncomeDetailDto>>
        {
            private readonly IIncomeRepository _incomeRepository;
            private readonly IMapper _mapper;

            public GetIncomeDetailDtoQueryHandler(IIncomeRepository incomeRepository, IMapper mapper)
            {
                _incomeRepository = incomeRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<IncomeDetailDto>> Handle(GetIncomeDetailDtoQuery request,
                CancellationToken cancellationToken)
            {
                var income = await _incomeRepository.Query().Include(x => x.Budget)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                var mappedData = _mapper.Map<IncomeDetailDto>(income);
                return new SuccessDataResult<IncomeDetailDto>(mappedData);
            }
        }
    }
}