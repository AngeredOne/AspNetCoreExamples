using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExceptionJournalApiExample.Domain.Models.Core;

[Index(nameof(TreeId))]
[Index(nameof(ParentId))]
[Index(nameof(Name), nameof(ParentId), IsUnique = true)]
public class Node : Entity
{
    [Required]
    public string Name { get; set; } = null!;

    public Guid? ParentId { get; set; }

    [ForeignKey(nameof(ParentId))]
    public Node? Parent { get; set; }

    public ICollection<Node> Children { get; set; } = new List<Node>();

    [Required]
    public Guid TreeId { get; set; }
    
    public string Payload { get; set; } = string.Empty;
}