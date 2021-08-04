using GloboEvent.Application.Contrats;
using GloboEvent.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GloboEvent.Persistence.IntegrationTest
{
    public class GloboEventDbContextTests
    {
        private readonly GloboEventDbContext _globoEventDbContext;
        private readonly Mock<ILoggedInUserService> _loggedInUserServiceMock;
        private readonly string _userId;

        public GloboEventDbContextTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<GloboEventDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()); 
            _userId = "4223E4CA-8C28-4623-9470-ED316D1D2E58";
            _loggedInUserServiceMock = new Mock<ILoggedInUserService>();
            _loggedInUserServiceMock.Setup(m => m.UserId).Returns(_userId);

            _globoEventDbContext = new GloboEventDbContext(dbContextOptions.Options, _loggedInUserServiceMock.Object);
        }

        [Fact]
        public async Task Save_SetCreatedByProperty()
        {
            var @event = new Event { Id = Guid.NewGuid(), Name = "Test Event" };

            _globoEventDbContext.Events.Add(@event);
            await _globoEventDbContext.SaveChangesAsync();

            @event.CreatedBy.ShouldBe(_userId);
        }
    }
}
