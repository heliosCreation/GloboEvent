using AutoMapper;
using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GloboEvent.Application.Features.Categories.Queries.GetCategoriesListWithEvent
{
    public class GetCategoriesListWithEventsQueryHandler : IRequestHandler<GetCategoriesListWithEventQuery, ApiResponse<CategoryWithEventsVm>>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoriesListWithEventsQueryHandler
            (IMapper mapper,
            ICategoryRepository categoryRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<ApiResponse<CategoryWithEventsVm>> Handle(GetCategoriesListWithEventQuery request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<CategoryWithEventsVm>();
            var list = await _categoryRepository.getAllWithEvents(request.IncludeHistory);
            response.DataList = _mapper.Map<List<CategoryWithEventsVm>>(list);
            return response;
        }
    }
}
