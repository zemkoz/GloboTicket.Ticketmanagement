using GloboTicket.TicketManagement.Application;
using GloboTickets.TicketManagement.Infrastructure;
using GloboTickets.TicketManagement.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GloboTicket.TicketManagement.Api;

public static class StartupExtensions
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddPersistenceServices(builder.Configuration);

        builder.Services.AddControllers();
        builder.Services.AddCors(options =>
        {
            var apiUrl = builder.Configuration["ApiUrl"] ?? "https://localhost:7132";
            options.AddPolicy("CorsPolicy", policy =>
            {
                policy.WithOrigins([apiUrl])
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(isOriginAllowed => true)
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            ResetDatabaseAsync(app);
            
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors("open");
        app.UseHttpsRedirection();
        app.MapControllers();
        
        return app;
    }
    
    private static void ResetDatabaseAsync(WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<GloboTicketDbContext>();
        context.Database.EnsureDeletedAsync();
        context.Database.Migrate();
    }
}
