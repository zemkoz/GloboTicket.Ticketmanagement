using GloboTicket.TicketManagement.Api;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices();

var app = builder.Build();
app.ConfigurePipeline();
app.Run();