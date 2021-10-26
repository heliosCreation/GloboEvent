using AutoMapper;
using GloboEvent.Application.Contracts.Signature;
using GloboEvent.Application.Contrats.Infrastructure;
using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Application.Model.Mail;
using GloboEvent.Application.Responses;
using GloboEvent.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GloboEvent.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, ApiResponse<CreateEventResponse>>, IValidatable
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;
        private readonly IEmailService _emailService;

        public CreateEventCommandHandler(
            IMapper mapper,
            IEventRepository eventRepository,
            IEmailService emailService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
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
