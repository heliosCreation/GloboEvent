using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboEvent.Application.Features.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<CreateCategoryCommandResponse>
    {
        public string Name { get; set; }
    }
}
