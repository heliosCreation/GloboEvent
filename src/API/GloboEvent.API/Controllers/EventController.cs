using GloboEvent.API.Attributes;
using GloboEvent.API.Contract;
using GloboEvent.Application.Features.Events.Commands.CreateEvent;
using GloboEvent.Application.Features.Events.Commands.DeleteEvent;
using GloboEvent.Application.Features.Events.Commands.UpdateEvent;
using GloboEvent.Application.Features.Events.Queries.GetEventDetails;
using GloboEvent.Application.Features.Events.Queries.GetEventExport;
using GloboEvent.Application.Features.Events.Queries.GetEventList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GloboEvent.API.Controllers
{
    using static ApiRoutes.Event;

    [Authorize]
    public class EventController : ApiController
    {
        [HttpGet(GetAll, Name = "Get all Event - Can be filtered to today's event with parameter.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Get(bool includeHistory)
        {
            var dtos = await Mediator.Send(new GetEventListQuery(includeHistory));
            return Ok(dtos);
        }

        [HttpGet(GetById, Name = "GetEventById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(Guid id)
        {
            var @event = await Mediator.Send(new GetEventDetailsQuery(id));
            return Ok(@event);
        }

        [HttpGet(ExportToCsv, Name = "ExportEvents")]
        [FileResultContentType("text/csv")]
        public async Task<FileResult> ExportEventsToCsv()
        {
            var response = await Mediator.Send(new GetEventExportQuery());
            return File(response.Data.Data, response.Data.ContentType, response.Data.EventExportFileName + ".csv");
        }

        [HttpPost(Create ,Name = "AddEvent")]
        public async Task<IActionResult> Add([FromBody] CreateEventCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }

        [HttpPut(Update, Name = "Update Event")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> EditEvent([FromBody] UpdateEventCommand command)
        {
            var x = await Mediator.Send(command);
            return Ok(x);
            //return Ok(await Mediator.Send(command));
        }

        [HttpDelete(Delete, Name = "Delete Event")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteEventCommand(id)));
        }
    }
}
