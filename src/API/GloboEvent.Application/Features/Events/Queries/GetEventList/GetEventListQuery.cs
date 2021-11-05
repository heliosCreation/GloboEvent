using GloboEvent.Application.Responses;
using MediatR;

namespace GloboEvent.Application.Features.Events.Queries.GetEventList
{
    public class GetEventListQuery : IRequest<ApiResponse<EventListVm>>
    {
        public bool IncludeHistory { get; set; }

        public GetEventListQuery(bool includeHistory)
        {
            IncludeHistory = includeHistory;
        }
    }
}
