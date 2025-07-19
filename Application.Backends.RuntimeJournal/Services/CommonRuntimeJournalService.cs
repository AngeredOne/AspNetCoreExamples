using Application.Backends.RuntimeJournal.Interfaces;
using Repository.RuntimeJournal;
using RuntimeJournalServices.Interfaces;

namespace Application.Backends.RuntimeJournal.Services;

/// <inheritdoc/>
public class CommonRuntimeJournalService(IJournalConstructionService constructionService, IRuntimeJournalRepository repo) : ICommonRuntimeJournalService
{
    public async Task<Guid> RegisterJournal(string header, object payload, Guid? parentJournalId = null)
    {
        var journal = await constructionService.ConstructCommonJournal(header, payload, parentJournalId);
        var registered = await repo.TryCreateAsync(journal);
        return registered;
    }
}