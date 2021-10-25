using GloboEvent.Application.Responses;
using MediatR;

namespace GloboEvent.Application.Features.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQuery : IRequest<ApiResponse<CategoryVm>>
    {
    }
}
