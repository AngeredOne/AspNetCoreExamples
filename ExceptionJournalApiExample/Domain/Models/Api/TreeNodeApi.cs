namespace ExceptionJournalApiExample.Domain.Models.Api;

public class TreeNodeApi
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public List<TreeNodeApi> Children { get; set; } = new();
}