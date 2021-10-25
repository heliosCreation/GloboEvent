using MediatR;
using System;

namespace GloboEvent.Application.Features.Events.Commands.DeleteEvent
{
    public class DeleteEventCommand : IRequest
    {
        public DeleteEventCommand(Guid eventId)
        {
            EventId = eventId;
        }
        public Guid EventId { get; set; }
    }
}
