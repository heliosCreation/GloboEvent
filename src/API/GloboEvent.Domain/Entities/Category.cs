using GloboEvent.Domain.Common;
using System.Collections.Generic;

namespace GloboEvent.Domain.Entities
{
    public class Category : AuditableEntity
    {
        public string Name { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
