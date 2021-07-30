﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboEvent.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommand : IRequest<Guid>
    {
        public string Name { get; set; }

        public int Price { get; set; }

        public string Artist { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public Guid CategoryId { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Price: {Price}, Artist: {Artist}, On: {Date.ToShortDateString()}, Description:{Description}";
        }
    }
}
