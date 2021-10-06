using System.Collections.Generic;
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
    public class GetBudgetListDtoQuery : IRequest<IDataResult<IEnumerable<BudgetDto>>>
    {
        public class
            GetBudgetListDtoQueryHandler : IRequestHandler<GetBudgetListDtoQuery, IDataResult<IEnumerable<BudgetDto>>>
        {
            private readonly IBudgetRepository _budgetRepository;
            private readonly IMapper _mapper;

            public GetBudgetListDtoQueryHandler(IBudgetRepository budgetRepository, IMapper mapper)
            {
                _budgetRepository = budgetRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<IEnumerable<BudgetDto>>> Handle(GetBudgetListDtoQuery request,
                CancellationToken cancellationToken)
            {
                var budget = await _budgetRepository.Query().Include(x => x.Category).ToListAsync(cancellationToken: cancellationToken);
                var mappedData = _mapper.Map<IEnumerable<BudgetDto>>(budget);
                return new SuccessDataResult<IEnumerable<BudgetDto>>(mappedData);
            }
        }
    }
}