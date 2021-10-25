using System;

namespace GloboEvent.Application.Features.Events.Queries.GetEventList
{
    public class EventListVm
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string ImageUrl { get; set; }
    }
}
