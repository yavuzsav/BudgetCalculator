using System;
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
    public class GetExpenseDetailDtoQuery : IRequest<IDataResult<ExpenseDetailDto>>
    {
        public Guid Id { get; set; }

        public class
            GetExpenseDetailDtoQueryHandler : IRequestHandler<GetExpenseDetailDtoQuery, IDataResult<ExpenseDetailDto>>
        {
            private readonly IExpenseRepository _expenseRepository;
            private readonly IMapper _mapper;

            public GetExpenseDetailDtoQueryHandler(IExpenseRepository expenseRepository, IMapper mapper)
            {
                _expenseRepository = expenseRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<ExpenseDetailDto>> Handle(GetExpenseDetailDtoQuery request,
                CancellationToken cancellationToken)
            {
                var expense = await _expenseRepository.Query().Include(x => x.Budget)
                    .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                var mappedData = _mapper.Map<ExpenseDetailDto>(expense);
                return new SuccessDataResult<ExpenseDetailDto>(mappedData);
            }
        }
    }
}