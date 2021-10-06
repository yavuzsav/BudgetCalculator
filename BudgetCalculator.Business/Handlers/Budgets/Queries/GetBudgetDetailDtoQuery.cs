using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BudgetCalculator.Core.Utilities.Results;
using BudgetCalculator.DataAccess.Interfaces;
using BudgetCalculator.Entities.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BudgetCalculator.Business.Handlers.Budgets.Queries
{
    public class GetBudgetDetailDtoQuery : IRequest<IDataResult<BudgetDetailDto>>
    {
        public Guid Id { get; set; }

        public class
            GetBudgetDetailDtoQueryHandler : IRequestHandler<GetBudgetDetailDtoQuery, IDataResult<BudgetDetailDto>>
        {
            private readonly IBudgetRepository _budgetRepository;
            private readonly IMapper _mapper;

            public GetBudgetDetailDtoQueryHandler(IBudgetRepository budgetRepository, IMapper mapper)
            {
                _budgetRepository = budgetRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<BudgetDetailDto>> Handle(GetBudgetDetailDtoQuery request,
                CancellationToken cancellationToken)
            {
                var budget = await _budgetRepository.Query().Include(x => x.Category).Include(x => x.Incomes)
                    .Include(x => x.Expenses).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                var mappedData = _mapper.Map<BudgetDetailDto>(budget);
                return new SuccessDataResult<BudgetDetailDto>(mappedData);
            }
        }
    }
}