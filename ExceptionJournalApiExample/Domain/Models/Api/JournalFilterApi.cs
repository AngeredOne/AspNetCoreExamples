namespace ExceptionJournalApiExample.Domain.Models.Api;

public class JournalFilterApi
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public string? Search { get; set; }
}