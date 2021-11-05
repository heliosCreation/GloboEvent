using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Application.Features.Events.Commands.DeleteEvent;
using GloboEvent.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Test.Utilities.DataSet;
using Xunit;

namespace GloboEvent.Application.UnitTests.Events.Commands
{
    using static EventSet;
    public class DeleteEventHandlerTests
    {
        private readonly Mock<IEventRepository> _mockEventRepository;
        private readonly DeleteEventCommandHandler _handler;
        public DeleteEventHandlerTests()
        {
            _mockEventRepository = new MockEventRepository().GetEntityRepository();
            _handler = new DeleteEventCommandHandler(_mockEventRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldDelete_WhenValidIdProvided()
        {
            var command = new DeleteEventCommand(EventId1);
            
            await _handler.Handle(command, CancellationToken.None);

            var target = await _mockEventRepository.Object.GetByIdAsync(EventId1);
            target.ShouldBeNull();
        }

        [Fact]
        public async Task Handle_ShouldNotDelete_WhenInvalidIdProvided_AndReturnsNotFoundApiResponse()
        {
            var command = new DeleteEventCommand(Guid.NewGuid());

            var result = await _handler.Handle(command, CancellationToken.None);
            var target = await _mockEventRepository.Object.GetByIdAsync(EventId1);

            result.StatusCode.ShouldBe((int)HttpStatusCode.NotFound);
            target.ShouldNotBeNull();
        }
    }
}
