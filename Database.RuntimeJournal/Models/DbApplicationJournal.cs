using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.RuntimeJournal.Models;

[Table("journals_n4", Schema = "journal")]
[Index(nameof(Id))]
[Index(nameof(JournalParentId))]
[Index(nameof(JournalHeader), nameof(JournalParentId), IsUnique = true)]
public class DbApplicationJournal : DbObject
{
    /// <summary>
    /// Идентификатор родительского журнала
    /// <remarks>Null в случае, если журнал является заглавным объектом</remarks>
    /// </summary>
    public Guid? JournalParentId { get; set; }
    
    /// <summary>
    /// Строковый заголовок журнала
    /// </summary>
    public string JournalHeader { get; set; } = null!;

    [ForeignKey(nameof(JournalParentId))]
    public DbApplicationJournal? Parent { get; set; }

    /// <summary>
    /// Все связанные дочерние журналы от текущего узла
    /// </summary>
    public ICollection<DbApplicationJournal> Children { get; set; } = new List<DbApplicationJournal>();
    
    /// <summary>
    /// Сериализованная полезная нагрузка журнала
    /// </summary>
    public string Payload { get; set; } = string.Empty;
}