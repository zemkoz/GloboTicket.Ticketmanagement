﻿using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.DeleteEvent;

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
{
    private readonly IAsyncRepository<Event> _eventRepository;

    public DeleteEventCommandHandler(IAsyncRepository<Event> eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var eventToDelete = await _eventRepository.GetByIdAsync(request.EventId);
        await _eventRepository.DeleteAsync(eventToDelete);
    }
}