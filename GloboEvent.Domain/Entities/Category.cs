using GloboEvent.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboEvent.Domain.Entities
{
    public class Category : AuditableEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
