﻿using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Categories.Queries.GetCategoriesListWithEvents
{
    public class GetCategoriesListWithEventsQuery : IRequest<List<CategoryWithEventListVm>>
    {
        public bool IncludeHistory { get; set; }
    }
}
