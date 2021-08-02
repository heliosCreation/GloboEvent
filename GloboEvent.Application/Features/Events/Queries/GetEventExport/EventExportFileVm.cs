using System;
using System.Collections.Generic;
using System.Text;

namespace GloboEvent.Application.Features.Events.Queries.GetEventExport
{
    public class EventExportFileVm
    {
        public string EventExportFileName { get; set; }

        public string ContentType { get; set; }

        public byte[] Data { get; set; }
    }
}
