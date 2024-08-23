using System.Net;
using GloboTicket.TicketManagement.Application.Contracts.Infrastructure;
using GloboTicket.TicketManagement.Application.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace GloboTickets.TicketManagement.Infrastructure.Mail;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    
    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }
    
    public async Task<bool> SendEmail(Email email)
    {
        var client = new SendGridClient(_emailSettings.ApiKey);

        var fromAddress = new EmailAddress()
        {
            Email = _emailSettings.FromAddress,
            Name = _emailSettings.FromName
        };
        var toAddress = new EmailAddress(email.To);
        
        var sendGridMessage = MailHelper.CreateSingleEmail(
            fromAddress, 
            toAddress, 
            email.Subject,
            email.Body, 
            email.Body);

        var response = await client.SendEmailAsync(sendGridMessage);
        return response.StatusCode is HttpStatusCode.Accepted or HttpStatusCode.OK;
    }
}