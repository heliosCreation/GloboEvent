using GloboEvent.Application.Features.Categories.Commands;
using GloboEvent.Application.Features.Categories.Queries.GetCategoriesList;
using GloboEvent.Application.Features.Categories.Queries.GetCategoriesListWithEvent;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GloboEvent.API.Controllers
{
    public class CategoryController : ApiController
    {
        [HttpGet("all", Name = "Get All Categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CategoryVm>>> Get()
        {
            var dtos = await Mediator.Send(new GetCategoriesListQuery());
            return Ok(dtos);
        }

        [HttpGet("allWithEvent", Name = "Get All Categories with Event")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CategoryWithEventsVm>>> GetWithEvent(bool includeHistory)
        {
            var dtos = await Mediator.Send(new GetCategoriesListWithEventQuery() { IncludeHistory = includeHistory });
            return Ok(dtos);
        }
        
        // POST api/<CategoryController>
        [HttpPost("addCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateCategoryCommandResponse>> AddCategory([FromBody] CreateCategoryCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}
