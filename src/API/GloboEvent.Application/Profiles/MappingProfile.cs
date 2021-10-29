﻿using AutoMapper;
using GloboEvent.Application.Features.Categories;
using GloboEvent.Application.Features.Categories.Commands.Create;
using GloboEvent.Application.Features.Categories.Commands.Update;
using GloboEvent.Application.Features.Categories.Queries.GetCategoryWithEvent;
using GloboEvent.Application.Features.Events.Commands.CreateEvent;
using GloboEvent.Application.Features.Events.Commands.UpdateEvent;
using GloboEvent.Application.Features.Events.Queries.GetEventDetails;
using GloboEvent.Application.Features.Events.Queries.GetEventExport;
using GloboEvent.Application.Features.Events.Queries.GetEventList;
using GloboEvent.Domain.Entities;

namespace GloboEvent.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventListVm>().ReverseMap();
            CreateMap<Event, EventDetailVm>()
                .ForMember(src => src.Category, opt => opt.MapFrom(src => src.Category))
                .ReverseMap();
            CreateMap<Event, EventExportDto>().ReverseMap();
            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<CreateEventCommand, Event>().ReverseMap();
            CreateMap<UpdateEventCommand, Event>();

            CreateMap<Category, CategoryVm>();
            CreateMap<Category, CategoryWithEventsVm>()
                .ForMember(src => src.Events, opt => opt.MapFrom(src => src.Events));
            CreateMap<Category, CreateCategoryCommand>();
            CreateMap<UpdateCategoryCommand, Category>();
        }
    }
}
