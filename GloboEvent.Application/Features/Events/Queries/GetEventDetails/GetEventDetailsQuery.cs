using MediatR;
using System;

namespace GloboEvent.Application.Features.Events.Queries.GetEventDetails
{
    public class GetEventDetailsQuery : IRequest<EventDetailVm>
    {
        public Guid Id { get; set; }

    }
}
