using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GloboEvent.Persistence.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(GloboEventDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<bool> IsUniqueNameAndDate(string name, DateTime date)
        {
            var matches = await _dbContext.Events.AnyAsync(e => e.Name.Equals(name) && e.Date.Equals(date));
            return matches == false;
        }

        public async Task<bool> IsUniqueNameAndDateForUpdate(string name, DateTime date, Guid id)
        {
            var matches = await _dbContext.Events.AnyAsync(e => e.Name.Equals(name) && e.Date.Equals(date) && e.Id != id);
            return matches == false;
        }
    }
}
