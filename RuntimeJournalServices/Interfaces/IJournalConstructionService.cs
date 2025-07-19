using Domain.RuntimeJournal.Models.Journal;

namespace RuntimeJournalServices.Interfaces;

/// <summary>
/// Базовый сервис для построения записей журнала
/// </summary>
public interface IJournalConstructionService
{
    
    /// <summary>
    /// Функция создания простого журнала
    /// </summary>
    /// <param name="header">Заголовок журнала</param>
    /// <param name="payload">Данные журнала</param>
    /// <param name="attachParentId">Идентификатор журнала для присоединения</param>
    /// <returns></returns>
    Task<ApplicationJournal> ConstructCommonJournal(string header, object payload, Guid? attachParentId = null);
    
    /// <summary>
    /// Функция создания журнала на основе исключения
    /// </summary>
    /// <param name="exception">Журналируемое исключение</param>
    /// <param name="includeInnerExceptions">Нужно ли журналировать вложенные исключения</param>
    /// <returns>Созданный объект журнала</returns>
    Task<ApplicationJournal> ConstructExceptionJournal(Exception exception, bool includeInnerExceptions = false);
    
    /// <summary>
    /// Функция создания журнала на основе исключения с переопределением заголовка
    /// </summary>
    /// <param name="customHeader">Пользовательский заголовок</param>
    /// <param name="exception">Журналируемое исключение</param>
    /// <param name="includeInnerExceptions">Нужно ли журналировать вложенные исключения</param>
    /// <returns>Созданный объект журнала</returns>
    Task<ApplicationJournal> ConstructExceptionJournal(string customHeader, Exception exception, bool includeInnerExceptions = false);
}