using System.ComponentModel.DataAnnotations;
using ExceptionJournalApiExample.Domain.Models.Core.Interfaces;

namespace ExceptionJournalApiExample.Domain.Models.Core;

public abstract class Entity : IEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.Empty;
}