using System.Net;
using System.Text.Json;
using ExceptionJournalApiExample.Domain.Models;
using ExceptionJournalApiExample.Domain.Models.Core;
using ExceptionJournalApiExample.Storage;

namespace ExceptionJournalApiExample.App.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, AppDbContext dbContext)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var eventId = DateTimeOffset.UtcNow.Ticks;
            var request = context.Request;
            var query = request.QueryString.HasValue ? request.QueryString.Value : null;

            string? body = null;
            if (request is { ContentLength: > 0, Body.CanSeek: true })
            {
                request.Body.Position = 0;
                using var reader = new StreamReader(request.Body);
                body = await reader.ReadToEndAsync();
                request.Body.Position = 0;
            }

            var journalEntry = new ExceptionJournal
            {
                EventId = eventId,
                CreatedAt = DateTime.UtcNow,
                ExceptionType = ex.GetType().Name,
                StackTrace = ex.ToString(),
                QueryParameters = query,
                Body = body
            };

            dbContext.ExceptionJournals.Add(journalEntry);
            await dbContext.SaveChangesAsync();

            var response = context.Response;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.ContentType = "application/json";

            if (ex is SecureException)
            {
                await response.WriteAsync(JsonSerializer.Serialize(new
                {
                    type = "Secure",
                    id = eventId.ToString(),
                    data = new { message = ex.Message }
                }));
            }
            else
            {
                await response.WriteAsync(JsonSerializer.Serialize(new
                {
                    type = "Exception",
                    id = eventId.ToString(),
                    data = new { message = $"Internal server error ID = {eventId}; Message: {ex.Message}" }
                }));
            }
        }
    }
}

public class SecureException : Exception
{
    public SecureException(string message) : base(message) { }
}
