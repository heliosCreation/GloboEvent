using GloboEvent.Application.Responses;
using MediatR;
using System;

namespace GloboEvent.Application.Features.Categories.Commands.Update
{
    public class UpdateCategoryCommand : IRequest<ApiResponse<object>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

    }
}
