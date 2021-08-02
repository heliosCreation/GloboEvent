using System;
using System.Collections.Generic;
using System.Text;

namespace GloboEvent.Application.Features.Events.Queries.GetEventExport
{
    public class EventExportDto
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
