using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Infrastructure;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Application.Exceptions;
using GloboTicket.TicketManagement.Application.Models;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.CreateEvent;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public CreateEventCommandHandler(IMapper mapper, 
        IEventRepository eventRepository, 
        IEmailService emailService)
    {
        _mapper = mapper;
        _eventRepository = eventRepository;
        _emailService = emailService;
    }

    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateEventCommandValidator();
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
        {
            throw new ValidationException(validationResult);
        }
        
        var @event = _mapper.Map<Event>(request);
        @event = await _eventRepository.AddAsync(@event);

        await SendNotificationEmailToAdmin(request);

        return @event.EventId;
    }

    private async Task SendNotificationEmailToAdmin(CreateEventCommand request)
    {
        var email = new Email()
        {
            To = "admin@test.com",
            Subject = "A new event was created",
            Body = $"A new event was created: {request}"
        };

        try
        {
            await _emailService.SendEmail(email);
        }
        catch (Exception e)
        {
            // This shouldn't stop the API from doing else so this can be logged.
        }
    }
}