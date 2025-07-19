
using Domain.RuntimeJournal.Models.Journal;

namespace Repository.RuntimeJournal;

/// <summary>
/// 
/// </summary>
public interface IRuntimeJournalRepository
{
    /// <summary>
    /// Записать журнал в постоянное хранилище
    /// </summary>
    /// <param name="journal"></param>
    /// <returns></returns>
    Task<Guid> TryCreateAsync(ApplicationJournal journal);

    /// <summary>
    /// Записать журнал как дочерний уже существующего
    /// </summary>
    /// <param name="journal"></param>
    /// <returns></returns>
    Task<Guid> TryAttachAsync(ApplicationJournal journal);

    /// <summary>
    /// Удалить журнал
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> TryRemoveAsync(Guid id);
}  