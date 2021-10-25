using MediatR;
using System.Collections.Generic;

namespace GloboEvent.Application.Features.Events.Queries.GetEventList
{
    public class GetEventListQuery : IRequest<List<EventListVm>>
    {
    }
}
