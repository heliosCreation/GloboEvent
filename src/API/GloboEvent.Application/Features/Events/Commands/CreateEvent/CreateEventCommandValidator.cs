using FluentValidation;
using GloboEvent.Application.Contrats.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GloboEvent.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        private readonly IEventRepository _eventRepository;

        public CreateEventCommandValidator(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} can't exceed 50 characters");

            RuleFor(p => p.Description)
                .MaximumLength(500).WithMessage("{PropertyName} can't exceed 500 characters.");

            RuleFor(p => p.Date)
                .Must(p => p >= DateTime.UtcNow).WithMessage("{PropertyName} must not be set in the past.")
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(p => p.Price)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required")
                .GreaterThan(0);

            RuleFor(e => e)
                .MustAsync(AreNameAndDateunique).WithMessage("An event with the same name and date already exist.");
        }

        private async Task<bool>AreNameAndDateunique(CreateEventCommand e, CancellationToken c)
        {
            return await _eventRepository.IsUniqueNameAndDate(e.Name, e.Date);
        }
    }
}
