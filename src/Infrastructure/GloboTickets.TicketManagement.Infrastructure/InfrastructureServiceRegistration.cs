using GloboTicket.TicketManagement.Application.Contracts.Infrastructure;
using GloboTickets.TicketManagement.Infrastructure.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GloboTickets.TicketManagement.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<EmailService>(configuration.GetSection("EmailSettings"));
        services.AddTransient<IEmailService, EmailService>();
        
        return services;
    }
}