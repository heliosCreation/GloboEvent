using AutoMapper;
using GloboEvent.Application.Behavior;
using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Application.Features.Events.Commands.CreateEvent;
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
using UnitTest.Utilities.DataSet;
using Xunit;

namespace GloboEvent.Application.UnitTests.Events.Commands
{
    using static EventSet;
    public class CreateEventHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IEventRepository> _mockEventRepository;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly CreateEventCommandHandler _handler;
        private readonly CreateEventCommandValidator _validator;
        public CreateEventHandlerTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
            _mockCategoryRepository = new MockCategoryRepository().GetEntityRepository();
            _mockEventRepository = new MockEventRepository().GetEntityRepository();
            _handler = new CreateEventCommandHandler(_mapper, _mockEventRepository.Object);
            _validator = new CreateEventCommandValidator(_mockEventRepository.Object, _mockCategoryRepository.Object);
        }

        [Fact]
        public async Task Handle_EventWhenValid_IsAddedToRepo()
        {
            var command = new CreateEventCommand()
            {
                Name = NewEvent.Name,
                Date = NewEvent.Date,
                Price = NewEvent.Price,
                CategoryId = NewEvent.CategoryId
            };
            var validationBehavior = new ValidationBehaviour<CreateEventCommand, ApiResponse<CreateEventResponse>>(new List<CreateEventCommandValidator>()
            {
                _validator
            });
            await validationBehavior.Handle(command, CancellationToken.None, () =>
            {
                return _handler.Handle(command, CancellationToken.None);
            });

            var allEvents = await _mockEventRepository.Object.ListAllAsync();
            allEvents.Count.ShouldBe(3);
        }

        [Theory]
        [ClassData(typeof(CreateEventInvalidCommand))]
        public async Task Handle_EventWhenInvalid_IsNotAddedToRepo_AndReturnsBadRequestInResponse(string name, int price, DateTime date, Guid id)
        {
            var command = new CreateEventCommand()
            {
                Name = name,
                Date = date,
                Price = price,
                CategoryId = id
            };
            var validationBehavior = new ValidationBehaviour<CreateEventCommand, ApiResponse<CreateEventResponse>>(new List<CreateEventCommandValidator>()
            {
                _validator
            });
            var result = await validationBehavior.Handle(command, CancellationToken.None, () =>
            {
                return _handler.Handle(command, CancellationToken.None);
            });

            result.StatusCode.ShouldBe((int)HttpStatusCode.BadRequest);

            var allEvents = await _mockEventRepository.Object.ListAllAsync();
            allEvents.Count.ShouldBe(2);
        }
    }
}
