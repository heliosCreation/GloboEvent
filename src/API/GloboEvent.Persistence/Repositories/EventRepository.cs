using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboEvent.Persistence.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(GloboEventDbContext dbContext): base(dbContext)
        {

        }
        public Task<bool> IsUniqueNameAndDate(string name, DateTime date)
        {
            var matches = _dbContext.Events.Any(e => e.Name.Equals(name) && e.Date.Equals(date));
            return Task.FromResult(matches);
        }
    }
}
