using GloboTicket.TicketManagement.Application.Features.Categories.Command.CreateCategory;
using GloboTicket.TicketManagement.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GloboTicket.TicketManagement.Api.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryWithEventListVm>>> GetCategories([FromQuery] bool includeHistory = false)
    {
        var categories =
            await _mediator.Send(new GetCategoriesListWithEventsQuery() { IncludeHistory = includeHistory });
        return Ok(categories);
    }
    
    [HttpPost]
    public async Task<ActionResult<CreatedCategoryVm>> CreateCategory([FromBody] CreateCategoryCommand createCategoryCommand)
    {
        var response = await _mediator.Send(createCategoryCommand);
        return Ok(response);
    }
}