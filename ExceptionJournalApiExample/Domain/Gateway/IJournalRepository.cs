using ExceptionJournalApiExample.Domain.Models.Core;

namespace ExceptionJournalApiExample.Domain.Gateway;

public interface IJournalRepository
{
    Task<Node> TryCreateAsync(string uName);
    Task<Node> TryAttachAsync(Guid parentId, string uName);
    Task<bool> TryRemoveAsync(Guid id);
    Task<bool> TryRenameAsync(Guid id, string newName);
}