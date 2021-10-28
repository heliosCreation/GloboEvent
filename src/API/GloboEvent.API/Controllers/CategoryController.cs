using GloboEvent.Application.Features.Categories.Commands.Create;
using GloboEvent.Application.Features.Categories.Commands.Delete;
using GloboEvent.Application.Features.Categories.Commands.Update;
using GloboEvent.Application.Features.Categories.Queries.GetCategoriesList;
using GloboEvent.Application.Features.Categories.Queries.GetCategoryWithEvent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


namespace GloboEvent.API.Controllers
{
    [Authorize]
    public class CategoryController : ApiController
    {
        [HttpGet("all", Name = "Get All Categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var dtos = await Mediator.Send(new GetCategoriesListQuery());
            return Ok(dtos);
        }

        [HttpGet("{id}/Event/{includeHistory}", Name = "Category with Events")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWithEvent(Guid id, bool includeHistory)
        {
            var dtos = await Mediator.Send(new GetCategoryWithEventQuery {Id = id, IncludeHistory = includeHistory });
            return Ok(dtos);
        }

        [HttpPost("addCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCategory([FromBody] CreateCategoryCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Put([FromBody] UpdateCategoryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete([FromBody] DeleteCategoryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
