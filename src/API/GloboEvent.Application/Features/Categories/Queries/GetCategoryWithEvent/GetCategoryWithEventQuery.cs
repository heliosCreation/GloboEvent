using GloboEvent.Application.Responses;
using MediatR;
using System;

namespace GloboEvent.Application.Features.Categories.Queries.GetCategoryWithEvent
{
    public class GetCategoryWithEventQuery : IRequest<ApiResponse<CategoryWithEventsVm>>
    {
        public Guid Id { get; set; }
        public bool IncludeHistory { get; set; }
    }
}
