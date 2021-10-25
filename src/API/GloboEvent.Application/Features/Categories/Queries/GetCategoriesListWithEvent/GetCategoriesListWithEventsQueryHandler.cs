using AutoMapper;
using GloboEvent.Application.Contrats.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GloboEvent.Application.Features.Categories.Queries.GetCategoriesListWithEvent
{
    public class GetCategoriesListWithEventsQueryHandler : IRequestHandler<GetCategoriesListWithEventQuery ,List<CategoryWithEventListVm>>
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

        public async Task<List<CategoryWithEventListVm>> Handle(GetCategoriesListWithEventQuery request, CancellationToken cancellationToken)
        {
            var list = await _categoryRepository.getAllWithEvents(request.IncludeHistory);
            return _mapper.Map<List<CategoryWithEventListVm>>(list);
        }
    }
}
