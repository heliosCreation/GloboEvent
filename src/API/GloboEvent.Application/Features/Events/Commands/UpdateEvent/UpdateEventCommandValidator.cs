using FluentValidation;
using GloboEvent.Application.Contrats.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GloboEvent.Application.Features.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
    {
        private readonly IEventRepository _eventRepository;

        public UpdateEventCommandValidator(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} can't exceed 50 characters");

            RuleFor(p => p.Description)
                .MaximumLength(500).WithMessage("{PropertyName} can't exceed 500 characters.");

            RuleFor(p => p.Date)
                .Must(p => p > DateTime.UtcNow).WithMessage("{PropertyName} must be set in the future.")
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(p => p.Price)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required")
                .GreaterThan(0);

            RuleFor(e => e)
                .MustAsync(AreNameAndDateuniqueForUpdate).WithMessage("An event with the same name and date already exist.");
            _eventRepository = eventRepository;
        }

        private async Task<bool> AreNameAndDateuniqueForUpdate(UpdateEventCommand e, CancellationToken c)
        {
            return await _eventRepository.IsUniqueNameAndDateForUpdate(e.Name, e.Date, e.Id);
        }
    }
}
