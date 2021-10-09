using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BudgetCalculator.Core.Utilities.Results;
using BudgetCalculator.DataAccess.Interfaces;
using BudgetCalculator.Entities.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BudgetCalculator.Business.Handlers.Expenses.Queries
{
    public class GetExpenseListDtoQuery : IRequest<IDataResult<IEnumerable<ExpenseDto>>>
    {
        public class
            GetExpenseListDtoQueryHandler : IRequestHandler<GetExpenseListDtoQuery,
                IDataResult<IEnumerable<ExpenseDto>>>
        {
            private readonly IExpenseRepository _expenseRepository;
            private readonly IMapper _mapper;

            public GetExpenseListDtoQueryHandler(IExpenseRepository expenseRepository, IMapper mapper)
            {
                _expenseRepository = expenseRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<IEnumerable<ExpenseDto>>> Handle(GetExpenseListDtoQuery request,
                CancellationToken cancellationToken)
            {
                var expenses = await _expenseRepository.Query().Include(x => x.Budget)
                    .ToListAsync(cancellationToken: cancellationToken);
                var mappedData = _mapper.Map<IEnumerable<ExpenseDto>>(expenses);
                return new SuccessDataResult<IEnumerable<ExpenseDto>>(mappedData);
            }
        }
    }
}