using GloboEvent.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GloboEvent.Application.Contrats.Persistence
{
    public interface ICategoryRepository : IAsyncRepository<Category>
    {
        Task<List<Category>> getAllWithEvents(bool includeHistory);

        Task<bool> IsNameUnique(string categoryName);
    }
}
