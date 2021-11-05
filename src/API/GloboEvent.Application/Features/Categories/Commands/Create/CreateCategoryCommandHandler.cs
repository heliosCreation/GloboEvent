using AutoMapper;
using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Application.Responses;
using GloboEvent.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GloboEvent.Application.Features.Categories.Commands.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ApiResponse<CategoryVm>>
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

        public async Task<ApiResponse<CategoryVm>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<CategoryVm>();
            var createdCategory = await _categoryRepository.AddAsync(new Category { Name = request.Name });
            response.Data = _mapper.Map<CategoryVm>(createdCategory);
            return response;
        }
    }
}
