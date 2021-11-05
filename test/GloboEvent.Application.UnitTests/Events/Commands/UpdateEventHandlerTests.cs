using AutoMapper;
using GloboEvent.Application.Behavior;
using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Application.Features.Categories.Commands.Update;
using GloboEvent.Application.Features.Events.Commands.UpdateEvent;
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

namespace GloboEvent.Application.UnitTests.Events.Commands
{
    using static EventSet;

    public class UpdateEventHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly Mock<IEventRepository> _mockEventRepository;
        private readonly UpdateEventCommandHandler _handler;
        private readonly UpdateEventCommandValidator _validator;
        public UpdateEventHandlerTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();

            _mockEventRepository = new MockEventRepository().GetEntityRepository();
            _mockCategoryRepository = new MockCategoryRepository().GetEntityRepository();
            _handler = new UpdateEventCommandHandler(_mapper,_mockEventRepository.Object);
            _validator = new UpdateEventCommandValidator(_mockEventRepository.Object, _mockCategoryRepository.Object);
        }

        [Fact]
        public async Task Handle_EventWhenValid_UpdatesRepo()
        {
            var command = new UpdateEventCommand
            {
                Id = EventId1,
                Name = NewEvent.Name,
                Date = NewEvent.Date,
                Price = NewEvent.Price,
                CategoryId = NewEvent.CategoryId
            };
            var validationBehavior = new ValidationBehaviour<UpdateEventCommand, ApiResponse<object>>(new List<UpdateEventCommandValidator>()
            {
                _validator
            });
            var result = await validationBehavior.Handle(command, CancellationToken.None, () =>
            {
                return _handler.Handle(command, CancellationToken.None);
            });

            var target = await _mockEventRepository.Object.GetByIdAsync(EventId1);
            target.Name.ShouldBe(NewEvent.Name);
            target.Date.ShouldBe(NewEvent.Date);
            target.Price.ShouldBe(NewEvent.Price);
            target.CategoryId.ShouldBe(NewEvent.CategoryId);
        }

        [Theory]
        [ClassData(typeof(UpdateEventInvalidCommand))]
        public async Task Handle_EventWhenInvalid_DoesNotUpdatesRepo_AndReturnsBadRequest(Guid id, string name, int price, DateTime date, Guid categoryId)
        {
            var command = new UpdateEventCommand
            {
                Id = id,
                Name = name,
                Date = date,
                Price = price,
                CategoryId = categoryId
            };

            var validationBehavior = new ValidationBehaviour<UpdateEventCommand, ApiResponse<object>>(new List<UpdateEventCommandValidator>()
            {
                _validator
            });
            var result = await validationBehavior.Handle(command, CancellationToken.None, () =>
            {
                return _handler.Handle(command, CancellationToken.None);
            });

            result.StatusCode.ShouldBe((int)HttpStatusCode.BadRequest);
            var target = await _mockEventRepository.Object.GetByIdAsync(EventId1);
            target.Name.ShouldBe(EventName1);
        }

        [Fact]
        public async Task Handle_EventWhenInvalidIdProvided_DoesNotUpdatesRepo_AnReturnsNotFound()
        {
            var command = new UpdateEventCommand
            {
                Id = Guid.NewGuid(),
                Name = NewEvent.Name,
                Date = NewEvent.Date,
                Price = NewEvent.Price,
                CategoryId = NewEvent.CategoryId
            };
            var validationBehavior = new ValidationBehaviour<UpdateEventCommand, ApiResponse<object>>(new List<UpdateEventCommandValidator>()
            {
                _validator
            });
            var result = await validationBehavior.Handle(command, CancellationToken.None, () =>
            {
                return _handler.Handle(command, CancellationToken.None);
            });
            var target = await _mockEventRepository.Object.GetByIdAsync(EventId1);


            result.StatusCode.ShouldBe((int)HttpStatusCode.NotFound);
            target.Name.ShouldBe(EventName1);
        }
    }

}
