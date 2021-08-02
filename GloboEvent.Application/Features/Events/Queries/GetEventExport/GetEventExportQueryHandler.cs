using AutoMapper;
using GloboEvent.Application.Contrats.Infrastructure;
using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GloboEvent.Application.Features.Events.Queries.GetEventExport
{

    public class GetEventExportQueryHandler : IRequestHandler<GetEventExportQuery, EventExportFileVm>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly ICsvExporterService _csvExporter;
        private readonly IMapper _mapper;

        public GetEventExportQueryHandler(
            IAsyncRepository<Event> eventRepository,
            ICsvExporterService csvExporter,
            IMapper mapper
            )
        {
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
            _csvExporter = csvExporter ?? throw new ArgumentNullException(nameof(csvExporter));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<EventExportFileVm> Handle(GetEventExportQuery request, CancellationToken cancellationToken)
        {
            var eventDto = _mapper.Map<List<EventExportDto>>((await _eventRepository.ListAllAsync()).OrderBy(e => e.Date));
            var csv = _csvExporter.ExportEventToCsv(eventDto);
            return new EventExportFileVm
            {
                EventExportFileName = $"Current list of event as for {DateTime.Today}",
                ContentType = "text/csv",
                Data = csv
            };
        }
    }
}
