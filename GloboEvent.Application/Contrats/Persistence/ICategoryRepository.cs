using GloboEvent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GloboEvent.Application.Contrats.Persistence
{
    public interface ICategoryRepository : IAsyncRepository<Category>
    {
        Task<List<Category>> getAllWithEvents(bool IncludeHistory);
    }
}
