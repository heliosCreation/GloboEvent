using System;

namespace GloboEvent.Application.Features.Categories.Commands
{
    public class CreateCategoryDto
    {
        public Guid CategoryId { get; set; }

        public string Name { get; set; }
    }
}