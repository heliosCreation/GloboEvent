using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GloboEvent.Application.Features.Events.Commands.DeleteEvent
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
    {
        private readonly IAsyncRepository<Event> _eventRepoistory;

        public DeleteEventCommandHandler(
            IAsyncRepository<Event> eventRepoistory)
        {
            _eventRepoistory = eventRepoistory ?? throw new ArgumentNullException(nameof(eventRepoistory));
        }
        public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var eventToDelete = await _eventRepoistory.GetByIdAsync(request.EventId);
            await _eventRepoistory.DeleteAsync(eventToDelete);
            return Unit.Value;
        }
    }
}
