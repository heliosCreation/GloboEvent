using GloboEvent.Application.Responses;
using MediatR;
using System;

namespace GloboEvent.Application.Features.Categories.Commands.Delete
{
    public class DeleteCategoryCommand : IRequest<ApiResponse<object>>
    {
        public Guid Id { get; set; }

        public DeleteCategoryCommand(Guid id)
        {
            Id = id;
        }
    }
}
