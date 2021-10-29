using GloboEvent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GloboEvent.Application.Contrats.Persistence
{
    public interface ICategoryRepository : IAsyncRepository<Category>
    {
        Task<Category> getWithEvents(bool includeHistory, Guid id);
        Task<bool> IsNameUnique(string categoryName);
        Task<bool> IsNameUniqueForUpdate(Guid id, string categoryName);
    }
}
