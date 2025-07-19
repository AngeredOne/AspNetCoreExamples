namespace Domain.RuntimeJournal.Models.Journal;

public class ApplicationJournal
{
    /// <summary>
    /// Уникальный идентификатор записи-журнала
    /// </summary>
    public Guid JournalId { get; set; }
    
    /// <summary>
    /// Идентификатор родительской записи
    /// <remarks>Null если является главным родителем</remarks>
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// Заголовок журнала
    /// </summary>
    public string JournalHeader { get; set; } = null!;
    
    /// <summary>
    /// Полезная загрузка журнала
    /// </summary>
    public string Payload { get; set; } = string.Empty;

    /// <summary>
    /// Вложенные журналы(ветви текущего корня дерева журнала)
    /// </summary>
    public List<ApplicationJournal> Inner { get; set; } = [];
}