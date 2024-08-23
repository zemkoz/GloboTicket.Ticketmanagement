using GloboTicket.TicketManagement.Application.Features.Events.Commands.CreateEvent;
using GloboTicket.TicketManagement.Application.Features.Events.Commands.DeleteEvent;
using GloboTicket.TicketManagement.Application.Features.Events.Commands.UpdateEvent;
using GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventDetail;
using GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventsList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GloboTicket.TicketManagement.Api.Controllers;

[ApiController]
[Route("api/events")]
public class EventController : ControllerBase
{
    private IMediator _mediator;

    public EventController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<EventVm>>> GetEvents()
    {
        var events = await _mediator.Send(new GetEventsListQuery());
        return Ok(events);
    }
    
    [HttpGet("{eventId}")]
    public async Task<ActionResult<EventDetailVm>> GetEventById(Guid eventId)
    {
        var getEventDetailQuery = new GetEventDetailQuery() { Id = eventId };
        var eventDetail = await _mediator.Send(getEventDetailQuery);
        return Ok(eventDetail);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateEvent([FromBody] CreateEventCommand createEventCommand)
    {
        var eventId = await _mediator.Send(createEventCommand);
        return Ok(
            new
            {
                id=eventId
            });
    }
    
    [HttpPut("{eventId}")]
    public async Task<ActionResult> UpdateEvent(Guid eventId, [FromBody] UpdateEventCommand updateEventCommand)
    {
        updateEventCommand.EventId = eventId;
        await _mediator.Send(updateEventCommand);
        return NoContent();
    }
    
    [HttpDelete("{eventId}")]
    public async Task<ActionResult> DeleteEvent(Guid eventId)
    {
        var deleteEventCommand = new DeleteEventCommand() { EventId = eventId };
        await _mediator.Send(deleteEventCommand);
        return NoContent();
    }
}