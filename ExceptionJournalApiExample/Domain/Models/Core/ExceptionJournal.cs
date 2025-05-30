using System.ComponentModel.DataAnnotations;
using ExceptionJournalApiExample.Domain.Models.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExceptionJournalApiExample.Domain.Models.Core;

[Index(nameof(EventId), IsUnique = true)]
[Index(nameof(CreatedAt))]
public class ExceptionJournal : Entity, IHasCreationTime
{
    [Required]
    public long EventId { get; set; } = DateTimeOffset.UtcNow.Ticks;

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public string ExceptionType { get; set; } = null!;

    [Required]
    public string StackTrace { get; set; } = null!;

    public string? QueryParameters { get; set; }

    public string? Body { get; set; }
}