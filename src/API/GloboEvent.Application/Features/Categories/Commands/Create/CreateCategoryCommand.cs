using GloboEvent.Application.Responses;
using MediatR;

namespace GloboEvent.Application.Features.Categories.Commands.Create
{
    public class CreateCategoryCommand : IRequest<ApiResponse<CategoryVm>>
    {
        public string Name { get; set; }
    }
}
