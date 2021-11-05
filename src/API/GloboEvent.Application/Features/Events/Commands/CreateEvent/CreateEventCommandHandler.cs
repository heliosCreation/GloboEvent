using AutoMapper;
using GloboEvent.Application.Contrats.Infrastructure;
using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Application.Responses;
using GloboEvent.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GloboEvent.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, ApiResponse<CreateEventResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;

        public CreateEventCommandHandler(
            IMapper mapper,
            IEventRepository eventRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        }
        public async Task<ApiResponse<CreateEventResponse>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<CreateEventResponse>();
            var @event = _mapper.Map<Event>(request);
            @event = await _eventRepository.AddAsync(@event);
            response.Data = new CreateEventResponse { Id = @event.Id };
            return response;
        }
    }
}
