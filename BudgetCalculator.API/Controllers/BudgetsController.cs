using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetCalculator.Business.Handlers.Budgets.Commands;
using BudgetCalculator.Business.Handlers.Budgets.Queries;
using BudgetCalculator.Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BudgetCalculator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetsController : BaseApiController
    {
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BudgetDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetBudgets()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetBudgetListDtoQuery()));
        }

        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BudgetDetailDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetBudgetById(Guid id)
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetBudgetDetailDtoQuery() { Id = id }));
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBudgetCommand createBudgetCommand)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(createBudgetCommand));
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EditBudgetCommand editBudgetCommand)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(editBudgetCommand));
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteBudgetCommand deleteBudgetCommand)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(deleteBudgetCommand));
        }
    }
}