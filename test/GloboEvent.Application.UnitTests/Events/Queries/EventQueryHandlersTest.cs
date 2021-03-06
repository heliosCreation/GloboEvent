using AutoMapper;
using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Application.Features.Events.Queries.GetEventDetails;
using GloboEvent.Application.Features.Events.Queries.GetEventList;
using GloboEvent.Application.Profiles;
using GloboEvent.Application.Responses;
using GloboEvent.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UnitTest.Utilities.DataSet;
using Xunit;

namespace GloboEvent.Application.UnitTests.Events.Queries
{
    using static EventSet;
    public class EventQueryHandlersTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IEventRepository> _mockCategoryRepository;

        public EventQueryHandlersTest()
        {
            _mockCategoryRepository = new MockEventRepository().GetEntityRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetEventList_shouldReturns_EntireCollection_AndCorrectCast()
        {
            var handler = new GetEventListQueryHandler(_mapper, _mockCategoryRepository.Object);

            var result = await handler.Handle(new GetEventListQuery(false), CancellationToken.None);

            result.ShouldBeOfType<ApiResponse<EventListVm>>();

            result.DataList.Count.ShouldBe(2);
        }

        [Fact]
        public async Task GetEventList_WithParamtersToTrue_shouldReturns_OnlyTodayEvents_AndCorrectCast()
        {
            var handler = new GetEventListQueryHandler(_mapper, _mockCategoryRepository.Object);

            var result = await handler.Handle(new GetEventListQuery(true), CancellationToken.None);

            result.ShouldBeOfType<ApiResponse<EventListVm>>();

            result.DataList.Count.ShouldBe(1);
            result.DataList.ForEach(e => e.Date.Date.ShouldBe(DateTime.Today.Date));
        }

        [Fact]
        public async Task GetEvent_shouldReturns_CorrectEvent_AndCorrectCast()
        {
            var handler = new GetEventtDetailQueryHandler(_mapper, _mockCategoryRepository.Object);

            var result = await handler.Handle(new GetEventDetailsQuery(EventId1), CancellationToken.None);

            result.ShouldBeOfType<ApiResponse<EventDetailVm>>();

            result.Data.Name.ShouldBe(EventName1);
        }

        [Fact]
        public async Task GetEvent_shouldReturns_NotFoundApiResponse()
        {
            var handler = new GetEventtDetailQueryHandler(_mapper, _mockCategoryRepository.Object);

            var result = await handler.Handle(new GetEventDetailsQuery(Guid.NewGuid()), CancellationToken.None);

            result.ShouldBeOfType<ApiResponse<EventDetailVm>>();
            result.Data.ShouldBeNull();
            result.StatusCode.ShouldBe((int)HttpStatusCode.NotFound);
        }
    }
}
