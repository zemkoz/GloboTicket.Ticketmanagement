using System.Net;
using System.Text.Json;
using GloboTicket.TicketManagement.Application.Exceptions;

namespace GloboTicket.TicketManagement.Api.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    
    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await ConvertException(context, exception);
        }
    }
    
    private Task ConvertException(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        HttpStatusCode httpStatutusCode;
        var result = string.Empty;
        
        switch (exception)
        {
            case ValidationException validationException:
                httpStatutusCode = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException);
                break;
            case BadRequestException badRequestException:
                httpStatutusCode = HttpStatusCode.BadRequest;
                result = exception.Message;
                break;
            case NotFoundException:
                httpStatutusCode = HttpStatusCode.NotFound;
                break;
            default:
                httpStatutusCode = HttpStatusCode.InternalServerError;
                break;
        }

        context.Response.StatusCode = (int) httpStatutusCode;
        return context.Response.WriteAsync(result);
    }
}