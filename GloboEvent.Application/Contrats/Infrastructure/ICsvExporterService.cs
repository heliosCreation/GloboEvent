using GloboEvent.Application.Features.Events.Queries.GetEventExport;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboEvent.Application.Contrats.Infrastructure
{
    public interface ICsvExporterService
    {
        byte[] ExportEventToCsv(List<EventExportDto> eventExportDtos);
    }
}
