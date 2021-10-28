using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GloboEvent.Persistence.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {

        public CategoryRepository(GloboEventDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Category> getWithEvents(bool includeHistory, Guid id)
        {
            if (includeHistory)
            {
                return await _dbContext
                    .Categories
                    .Where(c => c.Id == id)
                    .Include(c => c.Events)
                    .FirstOrDefaultAsync();
            }
            else
            {
                return await _dbContext.Categories
                 .Where(c => c.Id == id)
                .Include(c => c.Events.Where(c => c.Date == DateTime.Today))
                .FirstOrDefaultAsync();
            }
        }

        public async Task<bool> IsNameUnique(string categoryName)
        {
            var isUnique = await _dbContext.Categories.AnyAsync(c => c.Name == categoryName) == false;
            return isUnique;
        }
        public async Task<bool> IsNameUniqueForUpdate(Guid id, string categoryName)
        {
            return !await _dbContext.Categories.AnyAsync(c => c.Name == categoryName && c.Id != id );
        }
    }
}
