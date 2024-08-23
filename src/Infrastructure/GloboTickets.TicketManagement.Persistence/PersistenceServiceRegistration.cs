using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTickets.TicketManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GloboTickets.TicketManagement.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddDbContext<GloboTicketDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("GloboTicketManagementConnectionString")));

        services.AddScoped(typeof(Repositories.IAsyncRepository<>), typeof(BaseRepository<>));
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}