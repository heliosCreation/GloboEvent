using System;
using System.Collections.Generic;
using System.Text;

namespace GloboEvent.Application.Features.Categories.Queries.GetCategoriesListWithEvent
{
    public class CategoryWithEventListVm
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<CategoryEventDto> Events { get; set; }
    }
}
