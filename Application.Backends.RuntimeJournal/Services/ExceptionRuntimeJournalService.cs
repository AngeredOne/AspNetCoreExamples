using Application.Backends.RuntimeJournal.Interfaces;
using Repository.RuntimeJournal;
using RuntimeJournalServices.Interfaces;

namespace Application.Backends.RuntimeJournal.Services;

/// <inheritdoc/>
public class ExceptionRuntimeJournalService(IJournalConstructionService constructionService, IRuntimeJournalRepository repo) : IExceptionRuntimeJournalService
{
    public async Task<Guid> RegisterExceptionJournal(Exception exception)
    {
        var journal = await constructionService.ConstructExceptionJournal(exception, true);
        var registered = await repo.TryCreateAsync(journal);
        return registered;
    }

    public async Task<Guid> RegisterExceptionJournal(string customHeader, Exception exception)
    {
        var journal = await constructionService.ConstructExceptionJournal(customHeader, exception, true);
        var registered = await repo.TryCreateAsync(journal);
        return registered;
    }
}