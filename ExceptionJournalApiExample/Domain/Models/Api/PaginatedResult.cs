namespace ExceptionJournalApiExample.Domain.Models.Api;

public class PaginatedResult<T>
{
    public int Skip { get; set; }
    public int Count { get; set; }
    public List<T> Items { get; set; } = new();
}