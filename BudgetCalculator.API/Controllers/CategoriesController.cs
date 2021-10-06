using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetCalculator.Business.Handlers.Categories.Commands;
using BudgetCalculator.Business.Handlers.Categories.Queries;
using BudgetCalculator.Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BudgetCalculator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseApiController
    {
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CategoryDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetCategories()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetCategoryListDtoQuery()));
        }

        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetCategoryDtoQuery() { Id = id }));
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(createCategoryCommand));
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EditCategoryCommand editCategoryCommand)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(editCategoryCommand));
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteCategoryCommand deleteCategoryCommand)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(deleteCategoryCommand));
        }
    }
}