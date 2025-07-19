using System.Text.Json;
using Domain.RuntimeJournal.Models.Journal;
using RuntimeJournalServices.Interfaces;

namespace RuntimeJournalServices.Services;

/// <inheritdoc/>
public class JournalConstructionService : IJournalConstructionService
{
    public Task<ApplicationJournal> ConstructCommonJournal(string header, object payload, Guid? attachParentId = null)
    {
        var result = new ApplicationJournal()
        {
            JournalHeader = header,
            ParentId = attachParentId,
            Payload = JsonSerializer.Serialize(payload)
        };
        return Task.FromResult(result);
    }

    public Task<ApplicationJournal> ConstructExceptionJournal(Exception exception, bool includeInnerExceptions = false)
    {
        var result = new ApplicationJournal()
        {
            JournalHeader = exception.Source ?? exception.GetType().FullName ?? "Unknown exception",
            Payload = exception.Message,
            Inner = includeInnerExceptions && exception.InnerException is not null ? 
                [ConstructExceptionJournal(exception.InnerException, true).Result] : []
        };
        
        return Task.FromResult(result);
    }

    public Task<ApplicationJournal> ConstructExceptionJournal(string customHeader, Exception exception, bool includeInnerExceptions = false)
    {
        var constructExceptionJournal = ConstructExceptionJournal(exception, includeInnerExceptions).Result;
        constructExceptionJournal.JournalHeader = customHeader;
        
        return Task.FromResult(constructExceptionJournal);
    }
}