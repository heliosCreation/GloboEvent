using GloboEvent.Application.Responses;
using MediatR;

namespace GloboEvent.Application.Features.Categories.Queries.GetCategoriesListWithEvent
{
    public class GetCategoriesListWithEventQuery : IRequest<ApiResponse<CategoryWithEventsVm>>
    {
        public bool IncludeHistory { get; set; }
    }
}
