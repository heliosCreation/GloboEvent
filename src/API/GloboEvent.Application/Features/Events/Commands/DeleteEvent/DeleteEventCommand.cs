using GloboEvent.Application.Responses;
using MediatR;
using System;

namespace GloboEvent.Application.Features.Events.Commands.DeleteEvent
{
    public class DeleteEventCommand : IRequest<ApiResponse<object>>
    {
        public DeleteEventCommand(Guid eventId)
        {
            EventId = eventId;
        }
        public Guid EventId { get; set; }
    }
}
