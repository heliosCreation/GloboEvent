using GloboEvent.Application.Responses;
using MediatR;
using System;

namespace GloboEvent.Application.Features.Events.Queries.GetEventDetails
{
    public class GetEventDetailsQuery : IRequest<ApiResponse<EventDetailVm>>
    {
        public GetEventDetailsQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }

    }
}
