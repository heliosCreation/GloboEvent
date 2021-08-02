using CsvHelper;
using CsvHelper.Configuration;
using GloboEvent.Application.Contrats.Infrastructure;
using GloboEvent.Application.Features.Events.Queries.GetEventExport;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace GloboEvent.Infrastructure.FileExport
{
    public class CsvExporterService : ICsvExporterService
    {
        public byte[] ExportEventToCsv(List<EventExportDto> eventExportDtos)
        {
            using var memoryStream = new MemoryStream();
            using var streamWritter = new StreamWriter(memoryStream);
            using var csvWriter = new CsvWriter(streamWritter, CultureInfo.InvariantCulture);

            csvWriter.WriteField("EventId");
            csvWriter.WriteField("Name");
            csvWriter.WriteField("Date");
            csvWriter.NextRecord();

            foreach (var @event in eventExportDtos)
            {
                csvWriter.WriteField(@event.EventId);
                csvWriter.WriteField(@event.Name);
                csvWriter.WriteField(@event.Date);
                csvWriter.NextRecord();
            }

            streamWritter.Flush();
            var result = memoryStream.ToArray();

            return result;

        }
    }
}
