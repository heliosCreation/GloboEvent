using GloboEvent.Application.Features.Categories;
using GloboEvent.Application.Features.Categories.Commands.Create;
using GloboEvent.Application.Features.Categories.Commands.Update;
using GloboEvent.Application.Features.Categories.Queries.GetCategoryWithEvent;
using GloboEvent.Application.Features.Events.Commands.CreateEvent;
using GloboEvent.Application.Features.Events.Commands.UpdateEvent;
using GloboEvent.Application.Features.Events.Queries.GetEventDetails;
using GloboEvent.Application.Features.Events.Queries.GetEventExport;
using GloboEvent.Application.Features.Events.Queries.GetEventList;
using GloboEvent.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;


namespace GloboEvent.Application.UnitTests.Profiles
{
    public static class MappingClassData
    {
        public class MappingSets : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
                {
                    new object[] {typeof(Event),typeof(EventListVm) },
                    new object[] {typeof(Event),typeof(EventDetailVm) },
                    new object[] {typeof(Event),typeof(EventExportDto) },
                    new object[] {typeof(Event),typeof(EventDto) },
                    new object[] {typeof(CreateEventCommand),typeof(Event) },
                    new object[] {typeof(Event),typeof(EventListVm) },
                    new object[] {typeof(UpdateEventCommand),typeof(Event) },

                    new object[] {typeof(Category),typeof(CategoryVm) },
                    new object[] {typeof(Category),typeof(CategoryWithEventsVm) },
                    new object[] {typeof(CreateCategoryCommand),typeof(Category) },
                    new object[] {typeof(UpdateCategoryCommand),typeof(Category) },
                };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
