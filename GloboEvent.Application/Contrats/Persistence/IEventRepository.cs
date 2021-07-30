using GloboEvent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboEvent.Application.Contrats.Persistence
{
    public interface IEventRepository : IAsyncRepository<Event>
    {
    }
}
