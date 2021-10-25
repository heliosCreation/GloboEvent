using GloboEvent.Application.Responses;
using MediatR;

namespace GloboEvent.Application.Features.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<ApiResponse<CreateCategoryDto>>
    {
        public string Name { get; set; }
    }
}
