using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboEvent.Application.Features.Categories.Queries.GetCategoriesListWithEvent
{
    public class GetCategoriesListWithEventQuery : IRequest<List<CategoryWithEventListVm>>
    {
        public bool IncludeHistory { get; set; }
    }
}
