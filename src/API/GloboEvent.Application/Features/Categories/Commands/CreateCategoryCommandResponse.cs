using GloboEvent.Application.Responses;

namespace GloboEvent.Application.Features.Categories.Commands
{
    public class CreateCategoryCommandResponse : ApiResponse
    {
        public CreateCategoryCommandResponse() : base()
        {

        }

        public CreateCategoryDto Category { get; set; }
    }
}