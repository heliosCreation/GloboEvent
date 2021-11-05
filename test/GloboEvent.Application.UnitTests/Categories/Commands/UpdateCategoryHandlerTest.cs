using AutoMapper;
using GloboEvent.Application.Behavior;
using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Application.Features.Categories.Commands.Update;
using GloboEvent.Application.Features.Categories.Queries.GetCategoryWithEvent;
using GloboEvent.Application.Profiles;
using GloboEvent.Application.Responses;
using GloboEvent.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Test.Utilities.DataSet;
using Xunit;

namespace GloboEvent.Application.UnitTests.Categories.Commands
{
    using static CategorySet;
    public class UpdateCategoryHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly UpdateCategoryCommandHandler _handler;
        private readonly UpdateCategoryCommandValidator _validator;

        public UpdateCategoryHandlerTest()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();

            _mockCategoryRepository = new MockCategoryRepository().GetEntityRepository();
            _handler = new UpdateCategoryCommandHandler(_mockCategoryRepository.Object, _mapper);
            _validator = new UpdateCategoryCommandValidator(_mockCategoryRepository.Object);
        }

        [Fact]
        public async Task Handle_UpdateCategory_WhenValid_UpdatesAndReturnCorrectApiResponse()
        {
            var command = new UpdateCategoryCommand() { Name = "Test", Id = CategoryId1 };
            var validationBehavior = new ValidationBehaviour<UpdateCategoryCommand, ApiResponse<object>>(new List<UpdateCategoryCommandValidator>()
            {
                _validator
            });
            var result = await validationBehavior.Handle(command, CancellationToken.None, () =>
            {
                return _handler.Handle(command, CancellationToken.None);
            });

            result.StatusCode.ShouldBe((int)HttpStatusCode.OK);
            var Gethandler = new GetCategoryWithEventsQueryHandler(_mapper, _mockCategoryRepository.Object);

            var GetResult = await Gethandler.Handle(new GetCategoryWithEventQuery() { IncludeHistory = false, Id = CategoryId1 }, CancellationToken.None);
            GetResult.Data.Name.ShouldBe("Test");
        }

        [Theory]
        [ClassData(typeof(UpdateCategoryInvalidCommand))]
        public async Task Handle_UpdateCategory_WhenInvalidDataArePassed_ReturnsAppropriateResponse_AndDoesNotUpdate(Guid id, string data, int status)
        {
            var command = new UpdateCategoryCommand() { Name = data, Id = id };
            var validationBehavior = new ValidationBehaviour<UpdateCategoryCommand, ApiResponse<object>>(new List<UpdateCategoryCommandValidator>()
            {
                _validator
            });
            var result = await validationBehavior.Handle(command, CancellationToken.None, () =>
            {
                return _handler.Handle(command, CancellationToken.None);
            });

            result.StatusCode.ShouldBe(status);
            var Gethandler = new GetCategoryWithEventsQueryHandler(_mapper, _mockCategoryRepository.Object);

            var GetResult = await Gethandler.Handle(new GetCategoryWithEventQuery() { IncludeHistory = false, Id = CategoryId1 }, CancellationToken.None);
            GetResult.Data.Name.ShouldBe(CategoryName1);
        }
    }
}
