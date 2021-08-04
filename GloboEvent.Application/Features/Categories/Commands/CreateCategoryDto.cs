using System;

namespace GloboEvent.Application.Features.Categories.Commands
{
    public class CreateCategoryDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}