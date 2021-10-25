using System;
using System.Collections.Generic;

namespace GloboEvent.Application.Features.Categories.Queries.GetCategoriesListWithEvent
{
    public class CategoryWithEventsVm
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<EventDto> Events { get; set; }
    }
}
