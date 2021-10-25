using MediatR;

namespace GloboEvent.Application.Features.Events.Queries.GetEventExport
{
    public class GetEventExportQuery : IRequest<EventExportFileVm>
    {
    }
}
