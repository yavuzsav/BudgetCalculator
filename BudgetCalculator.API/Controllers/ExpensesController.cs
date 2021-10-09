using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetCalculator.Business.Handlers.Expenses.Commands;
using BudgetCalculator.Business.Handlers.Expenses.Queries;
using BudgetCalculator.Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BudgetCalculator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : BaseApiController
    {
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ExpenseDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetExpenses()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetExpenseListDtoQuery()));
        }

        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExpenseDetailDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetExpenseById(Guid id)
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetExpenseDetailDtoQuery() { Id = id }));
        }

        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ExpenseDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("GetByBudgetId/{budgetId}")]
        public async Task<IActionResult> GetExpenseByBudgetId(Guid budgetId)
        {
            return GetResponseOnlyResultData(
                await Mediator.Send(new GetExpenseByBudgetIdQuery() { BudgetId = budgetId }));
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateExpenseCommand createExpenseCommand)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(createExpenseCommand));
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EditExpenseCommand editExpenseCommand)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(editExpenseCommand));
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteExpenseCommand deleteExpenseCommand)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(deleteExpenseCommand));
        }
    }
}