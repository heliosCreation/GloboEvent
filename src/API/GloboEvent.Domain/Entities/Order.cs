﻿using GloboEvent.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboEvent.Domain.Entities
{
    public class Order : AuditableEntity
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public int OrderTotal { get; set; }

        public DateTime OrderPlaced { get; set; }

        public bool OrderPaid { get; set; }
    }
}