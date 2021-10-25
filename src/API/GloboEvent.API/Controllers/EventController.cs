using GloboEvent.API.Attributes;
using GloboEvent.Application.Features.Events.Commands.CreateEvent;
using GloboEvent.Application.Features.Events.Commands.DeleteEvent;
using GloboEvent.Application.Features.Events.Commands.UpdateEvent;
using GloboEvent.Application.Features.Events.Queries.GetEventDetails;
using GloboEvent.Application.Features.Events.Queries.GetEventExport;
using GloboEvent.Application.Features.Events.Queries.GetEventList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GloboEvent.API.Controllers
{
    public class EventController : ApiController
    {
        // GET: api/<EventController>
        [HttpGet("all", Name = "Get all Event")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<EventListVm>>> Get()
        {
            var dtos = await Mediator.Send(new GetEventListQuery());
            return Ok(dtos);
        }

        // GET api/<EventController>/5
        [HttpGet("{id}", Name = "GetEventById")]
        public async Task<ActionResult<EventDetailVm>> Get(Guid id)
        {
            var @event = await Mediator.Send(new GetEventDetailsQuery(id));
            return Ok(@event);
        }

        [HttpGet("export",Name = "ExportEvents")]
        [FileResultContentType("text/csv")]
        public async Task<FileResult>ExportEventsToCsv()
        {
            var fileDto = await Mediator.Send(new GetEventExportQuery());
            return File(fileDto.Data, fileDto.ContentType, fileDto.EventExportFileName +".csv");
        }

        // POST api/<EventController>
        [HttpPost(Name = "AddEvent")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateEventCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }

        // PUT api/<EventController>/5
        [HttpPut(Name ="Update Event")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateEventCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        // DELETE api/<EventController>/5
        [HttpDelete("{id}", Name = "Delete Event")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteEventCommand(id));
            return NoContent();
        }
    }
}
