using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetCalculator.Business.Handlers.Incomes.Commands;
using BudgetCalculator.Business.Handlers.Incomes.Queries;
using BudgetCalculator.Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BudgetCalculator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomesController : BaseApiController
    {
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<IncomeDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetIncomes()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetIncomeListDtoQuery()));
        }

        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IncomeDetailDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetIncomeById(Guid id)
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetIncomeDetailDtoQuery() { Id = id }));
        }

        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<IncomeDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("GetByBudgetId/{budgetId}")]
        public async Task<IActionResult> GetIncomeByBudgetId(Guid budgetId)
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetIncomeByBudgetQuery() { BudgetId = budgetId }));
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateIncomeCommand createIncomeCommand)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(createIncomeCommand));
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EditIncomeCommand editIncomeCommand)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(editIncomeCommand));
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteIncomeCommand deleteIncomeCommand)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(deleteIncomeCommand));
        }
    }
}