using AutoMapper;
using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Application.Responses;
using GloboEvent.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GloboEvent.Application.Features.Events.Queries.GetEventList
{
    public class GetEventListQueryHandler : IRequestHandler<GetEventListQuery, ApiResponse<EventListVm>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Event> _eventRepository;

        public GetEventListQueryHandler(
            IMapper mapper,
            IAsyncRepository<Event> eventRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
        }
        public async Task<ApiResponse<EventListVm>> Handle(GetEventListQuery request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<EventListVm>();
            var allEvent = (await _eventRepository.ListAllAsync()).OrderBy(x => x.Date);
            response.DataList = _mapper.Map<List<EventListVm>>(allEvent);
            return response;
        }
    }
}
