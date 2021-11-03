using AutoMapper;
using FluentValidation;
using GloboEvent.Application.Behavior;
using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Application.Features.Categories;
using GloboEvent.Application.Features.Categories.Commands.Create;
using GloboEvent.Application.Profiles;
using GloboEvent.Application.Responses;
using GloboEvent.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Test.Utilities.DataSet;
using Xunit;

namespace GloboEvent.Application.UnitTests.Categories.Commands
{
    using static CategorySet;
    public class CreateCategoryTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly CreateCategoryCommandHandler _handler;
        private readonly CreateCategoryCommandValidator _validator;
        public CreateCategoryTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();

            _mockCategoryRepository = new MockCategoryRepository().GetEntityRepository();
            _handler = new CreateCategoryCommandHandler(_mapper, _mockCategoryRepository.Object);
            _validator = new CreateCategoryCommandValidator(_mockCategoryRepository.Object);
        }

        [Fact]
        public async Task Handle_CategoryWhenValid_IsAddedToRepo()
        {
            var command = new CreateCategoryCommand() { Name = "Test" };
            var validationBehavior = new ValidationBehaviour<CreateCategoryCommand, ApiResponse<CategoryVm>>(new List<CreateCategoryCommandValidator>()
            {
                new CreateCategoryCommandValidator(_mockCategoryRepository.Object)
            });
            var result = await validationBehavior.Handle(command, CancellationToken.None, () =>
            {
                return _handler.Handle(command, CancellationToken.None);
            });

            var allCategories = await _mockCategoryRepository.Object.ListAllAsync();

            allCategories.Count.ShouldBe(3);
            result.Data.Name.ShouldBe("Test");
        }

        [Theory]
        [ClassData(typeof(CreateCategoryInvalidCommand))]
        public async Task Handle_CategoryWhenInValid_IsNotAddedToRepo(string name)
        {
            var command = new CreateCategoryCommand() { Name = name };
            var validationBehavior = new ValidationBehaviour<CreateCategoryCommand, ApiResponse<CategoryVm>>(new List<CreateCategoryCommandValidator>()
            {
                new CreateCategoryCommandValidator(_mockCategoryRepository.Object)
            });
            var result = await validationBehavior.Handle(command , CancellationToken.None, () =>
            {
                return _handler.Handle(command, CancellationToken.None);
            });

             
            var allCategories = await _mockCategoryRepository.Object.ListAllAsync();

            allCategories.Count.ShouldBe(2);
            result.StatusCode.ShouldBe((int)HttpStatusCode.BadRequest);
            result.ErrorMessages.ShouldNotBeNull();
        }
    }
}
