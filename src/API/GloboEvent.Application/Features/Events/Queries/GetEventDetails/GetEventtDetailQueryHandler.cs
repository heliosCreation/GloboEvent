using AutoMapper;
using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Application.Responses;
using GloboEvent.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GloboEvent.Application.Features.Events.Queries.GetEventDetails
{
    public class GetEventtDetailQueryHandler : IRequestHandler<GetEventDetailsQuery, ApiResponse<EventDetailVm>>
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;
        private readonly IAsyncRepository<Category> _categoryRepository;

        public GetEventtDetailQueryHandler(
            IMapper mapper,
            IEventRepository eventRepository,
            IAsyncRepository<Category> categoryRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<ApiResponse<EventDetailVm>> Handle(GetEventDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<EventDetailVm>();
            var @event = await _eventRepository.GetByIdAsync(request.Id);
            if (@event == null)
            {
                return response.setNotFoundResponse();
            }
            var eventDetailDto = _mapper.Map<EventDetailVm>(@event);

            response.Data = eventDetailDto;

            return response; ;
        }
    }
}
