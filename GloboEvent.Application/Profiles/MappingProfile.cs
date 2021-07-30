using AutoMapper;
using GloboEvent.Application.Features.Categories.Queries.GetCategoriesList;
using GloboEvent.Application.Features.Categories.Queries.GetCategoriesListWithEvent;
using GloboEvent.Application.Features.Events.Queries.GetEventDetails;
using GloboEvent.Application.Features.Events.Queries.GetEventList;
using GloboEvent.Domain.Entities;
using System.Collections.Generic;

namespace GloboEvent.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventListVm>().ReverseMap();
            CreateMap<Event, EventDetailVm>().ReverseMap();
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryListVm>();
            CreateMap<Category, CategoryWithEventListVm>();
            CreateMap<List<Category>, List<CategoryListVm>>();
        }
    }
}
