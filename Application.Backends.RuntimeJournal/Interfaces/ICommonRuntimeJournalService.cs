namespace Application.Backends.RuntimeJournal.Interfaces;

/// <summary>
/// Простой бекенд для работы с журналом
/// </summary>
public interface ICommonRuntimeJournalService
{
    /// <summary>
    /// Создать и записать в хранилище новый журнал
    /// </summary>
    /// <param name="header">Строковый заголовок</param>
    /// <param
    /// name="payload">Объект полезной нагрузки
    /// <remarks>Сериализуется в json строку</remarks>
    /// </param>
    /// <param
    /// name="parentJournalId">Идентификатор журнала, являющегося родительским
    /// <remarks>Null при отсутствии родительского журнала</remarks>
    /// </param>
    /// <returns>Идентификатор записанного журнала в хранилище</returns>
    Task<Guid> RegisterJournal(string header, object payload, Guid? parentJournalId = null);
}