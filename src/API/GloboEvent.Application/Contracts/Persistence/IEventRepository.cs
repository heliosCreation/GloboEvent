using GloboEvent.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace GloboEvent.Application.Contrats.Persistence
{
    public interface IEventRepository : IAsyncRepository<Event>
    {
        Task<bool> IsUniqueNameAndDate(string name, DateTime date);
    }
}
