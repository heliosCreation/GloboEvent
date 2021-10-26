using FluentValidation;
using GloboEvent.Application.Contrats.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GloboEvent.Application.Features.Categories.Commands.Create
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryCommandValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(e => e)
                .MustAsync(IsNameUnique).WithMessage("A category with the same given name already exists.");
        }

        private async Task<bool> IsNameUnique(CreateCategoryCommand e, CancellationToken c)
        {
            return await _categoryRepository.IsNameUnique(e.Name);
        }
    }
}
