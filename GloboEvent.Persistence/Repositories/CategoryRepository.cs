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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {

        public CategoryRepository(GloboEventDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Category>> getAllWithEvents(bool includeHistory)
        {
            var categoriesWithEvent = new List<Category>();
            if (includeHistory)
            {
                categoriesWithEvent = await _dbContext.Categories.Include(c => c.Events).ToListAsync();
            }
            else
            {
                categoriesWithEvent = 
                    await _dbContext.Categories
                    .Include(c => c.Events.Where(c => c.Date == DateTime.Today))
                    .ToListAsync();
            }

            return categoriesWithEvent; 
        }

        public async Task<bool> IsNameUnique(string categoryName)
        {
            var isUnique =  await _dbContext.Categories.AnyAsync(c => c.Name == categoryName) == false;
            return isUnique; 
        }
    }
}
