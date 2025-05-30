namespace ExceptionJournalApiExample.Domain.Models.Api;

public class ExceptionJournalApi
{
    public Guid Id { get; set; }
    public long EventId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Text { get; set; } = null!;
}