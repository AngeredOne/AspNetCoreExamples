namespace Application.Backends.RuntimeJournal.Interfaces;

/// <summary>
/// Расширенный бекенд по работе с журналом
/// Позволяет обрабатывать исключения
/// </summary>
public interface IExceptionRuntimeJournalService
{
    /// <summary>
    /// Создать журнал на основе исключения
    /// </summary>
    /// <param name="exception">Объект исключения для заполнения журнала</param>
    /// <returns>Идентификатор записанного журнала в хранилище</returns>
    Task<Guid> RegisterExceptionJournal(Exception exception);
    
    /// <summary>
    /// Создать журнал на основе исключения с переопределением заголовка
    /// </summary>
    /// <param name="customHeader">Пользовательский заголовок</param>
    /// <param name="exception">Объект исключения для заполнения журнала</param>
    /// <returns>Идентификатор записанного журнала в хранилище</returns>
    Task<Guid> RegisterExceptionJournal(string customHeader, Exception exception);
}