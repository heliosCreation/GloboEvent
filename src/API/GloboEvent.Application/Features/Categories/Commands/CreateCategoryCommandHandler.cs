using AutoMapper;
using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GloboEvent.Application.Features.Categories.Commands
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryCommandHandler(
            IMapper mapper,
            ICategoryRepository categoryRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var createCategoryResponse = new CreateCategoryCommandResponse();

            var validator = new CreateCategoryCommandValidator(_categoryRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                var errors = validationResult.Errors.Select(v => v.ErrorMessage);
                createCategoryResponse.Success = false;
                createCategoryResponse.ErrorMessages = errors;
            }

            if (createCategoryResponse.Success)
            {
                var createdCategory = await _categoryRepository.AddAsync(new Category { Name = request.Name });
                createCategoryResponse.Category = _mapper.Map<CreateCategoryDto>(createdCategory);
            }

            return createCategoryResponse;
        }
    }
}
