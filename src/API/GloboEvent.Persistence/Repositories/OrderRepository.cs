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
    public class OrderRepository : BaseRepository<Order>, IOrderRepository 
    {
        public OrderRepository(GloboEventDbContext dbContext):base(dbContext)
        {

        }

        public async Task<List<Order>> GetPagedOrderForMonth(DateTime date, int page, int size)
        {
            return await _dbContext.Orders
                .Where(o => o.OrderPlaced.Month == date.Month && o.OrderPlaced.Year == date.Year)
                .Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
        }

        public async Task<int> GetTotalCountOfOrderForMonth(DateTime date)
        {
            return await _dbContext.Orders.CountAsync(o => o.OrderPlaced.Month == date.Month && o.OrderPlaced.Year == date.Year);
        }
    }
}
