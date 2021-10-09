using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GetExpenseByBudgetIdQuery : IRequest<IDataResult<IEnumerable<ExpenseDto>>>
    {
        public Guid BudgetId { get; set; }

        public class
            GetExpenseByBudgetIdQueryHandler : IRequestHandler<GetExpenseByBudgetIdQuery,
                IDataResult<IEnumerable<ExpenseDto>>>
        {
            private readonly IExpenseRepository _expenseRepository;
            private readonly IMapper _mapper;

            public GetExpenseByBudgetIdQueryHandler(IExpenseRepository expenseRepository, IMapper mapper)
            {
                _expenseRepository = expenseRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<IEnumerable<ExpenseDto>>> Handle(GetExpenseByBudgetIdQuery request,
                CancellationToken cancellationToken)
            {
                var expenses = await _expenseRepository.Query().Include(x => x.Budget)
                    .Where(x => x.BudgetId == request.BudgetId).ToListAsync(cancellationToken);
                var mappedData = _mapper.Map<IEnumerable<ExpenseDto>>(expenses);
                return new SuccessDataResult<IEnumerable<ExpenseDto>>(mappedData);
            }
        }
    }
}