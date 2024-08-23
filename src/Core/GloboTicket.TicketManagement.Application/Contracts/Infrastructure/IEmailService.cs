using GloboTicket.TicketManagement.Application.Models;

namespace GloboTicket.TicketManagement.Application.Contracts.Infrastructure;

public interface IEmailService
{
    Task<bool> SendEmail(Email email);
}