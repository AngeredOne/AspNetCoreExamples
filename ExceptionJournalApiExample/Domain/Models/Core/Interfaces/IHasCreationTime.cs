namespace ExceptionJournalApiExample.Domain.Models.Core.Interfaces;

public interface IHasCreationTime
{
    DateTime CreatedAt { get; set; }
}