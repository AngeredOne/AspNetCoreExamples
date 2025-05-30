namespace ExceptionJournalApiExample.Domain.Models.Api;

public class ExceptionJournalInfoApi
{
    public Guid Id { get; set; }
    public long EventId { get; set; }
    public DateTime CreatedAt { get; set; }
}