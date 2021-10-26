using AutoMapper;
using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GloboEvent.Application.Features.Categories.Queries.GetCategoryWithEvent
{
    public class GetCategoryWithEventsQueryHandler : IRequestHandler<GetCategoryWithEventQuery, ApiResponse<CategoryWithEventsVm>>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryWithEventsQueryHandler
            (IMapper mapper,
            ICategoryRepository categoryRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<ApiResponse<CategoryWithEventsVm>> Handle(GetCategoryWithEventQuery request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<CategoryWithEventsVm>();
            var list = await _categoryRepository.getWithEvents(request.IncludeHistory, request.Id);
            response.Data = _mapper.Map<CategoryWithEventsVm>(list);
            return response;
        }
    }
}
