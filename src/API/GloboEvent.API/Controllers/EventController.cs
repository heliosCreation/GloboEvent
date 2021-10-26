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
        [HttpGet("all", Name = "Get all Event")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Get()
        {
            var dtos = await Mediator.Send(new GetEventListQuery());
            return Ok(dtos);
        }

        [HttpGet("{id}", Name = "GetEventById")]
        public async Task<IActionResult> Get(Guid id)
        {
            var @event = await Mediator.Send(new GetEventDetailsQuery(id));
            return Ok(@event);
        }

        [HttpGet("export",Name = "ExportEvents")]
        [FileResultContentType("text/csv")]
        public async Task<FileResult>ExportEventsToCsv()
        {
            var response = await Mediator.Send(new GetEventExportQuery());
            return File(response.Data.Data, response.Data.ContentType, response.Data.EventExportFileName +".csv");
        }

        [HttpPost(Name = "AddEvent")]
        public async Task<IActionResult> Create([FromBody] CreateEventCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }

        [HttpPut(Name ="Update Event")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update([FromBody] UpdateEventCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "Delete Event")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteEventCommand(id));
            return NoContent();
        }
    }
}
