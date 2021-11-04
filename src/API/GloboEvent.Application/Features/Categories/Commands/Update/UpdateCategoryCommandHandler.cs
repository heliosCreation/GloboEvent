﻿using AutoMapper;
using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Application.Responses;
using GloboEvent.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GloboEvent.Application.Features.Categories.Commands.Update
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ApiResponse<object>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository ?? throw new System.ArgumentNullException(nameof(categoryRepository));
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }
        public async Task<ApiResponse<object>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<object>();
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category == null)
            {
                return response.setNotFoundResponse($"Category with id {request.Id} was not found");
            }
            var test = _mapper.Map<Category>(request);
            _mapper.Map(request, category, typeof(UpdateCategoryCommand), typeof(Category));
            await _categoryRepository.UpdateAsync(category);

            return response;
        }
    }
}
