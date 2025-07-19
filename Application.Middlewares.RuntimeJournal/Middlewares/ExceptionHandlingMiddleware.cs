using System.Net;
using System.Text.Json;
using Application.Backends.RuntimeJournal.Interfaces;
using Application.Middlewares.RuntimeJournal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Middlewares.RuntimeJournal.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IExceptionRuntimeJournalService journalService)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            Guid jId;
            
            var response = context.Response;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.ContentType = "application/json";

            // Если исключение помечено как Secret -> мы скрываем детали отовсюду, записав жрунал
            if (ex is SecureJournalException)
            {
                
                jId = await journalService.RegisterExceptionJournal("SECURE", ex);
                
                await response.WriteAsync(JsonSerializer.Serialize(new
                {
                    type = "Secure exception",
                    data = new { message = "Server secure exception; Details not allowed here" }
                }));
            }
            // Если это любое другое исключение - можно предоставить какие-то детали, например, идентификатор журнала.
            else
            {
                jId = await journalService.RegisterExceptionJournal(ex);
                await response.WriteAsync(JsonSerializer.Serialize(new
                {
                    type = "Exception",
                    data = new { message = $"Internal server error! Details stored at journal: {jId}" }
                }));
            }
            
            // В любом случае, логируем детали, т.к. лог доступен только разработке
            logger.LogWarning(ex, $"Register exception at journal {jId}.\n" +
                                  $"Info: {ex.Message}");
        }
    }
}
