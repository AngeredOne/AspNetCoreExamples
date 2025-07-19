using Database.RuntimeJournal.Context;
using Database.RuntimeJournal.Models;
using Domain.RuntimeJournal.Models.Journal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Repository.RuntimeJournal;

namespace Database.RuntimeJournal.Repository;

public class JournalRepository(RuntimeExceptionsDatabaseContext db) : IRuntimeJournalRepository
{
    
    // TODO: MAPPING; OnModelCreating for ID
    private DbApplicationJournal CreateDbEntry(ApplicationJournal journal)
    {
        var dbEntry = new DbApplicationJournal()
        {
            Id = Guid.NewGuid(),
            JournalParentId = journal.ParentId,
            Payload = journal.Payload,
            JournalHeader = journal.JournalHeader,
            Children = journal.Inner.Select(CreateDbEntry).ToList()
        };
        
        return dbEntry;
    }
    
    public async Task<Guid> TryCreateAsync(ApplicationJournal journal)
    {
        var dbJournal = CreateDbEntry(journal);
        db.Nodes.Add(dbJournal);
        await db.SaveChangesAsync();
        return dbJournal.Id;
    }
    
    
    public async Task<Guid> TryAttachAsync(ApplicationJournal journal)
    {
        if(journal.ParentId == Guid.Empty)
            throw new Exception("Guid is not a valid journal id(empty)");

        var parent = await db.Nodes.FirstOrDefaultAsync(n => n.Id == journal.ParentId);
        if (parent == null) throw new Exception("Parent journal not found");

        // if (parent.Children.Any(c => c.Name == uName))
        //     throw new Exception("Node name must be unique among siblings");

        var attachedJournal = CreateDbEntry(journal);

        db.Nodes.Add(attachedJournal);
        await db.SaveChangesAsync();
        
        return attachedJournal.Id;
    }

    public async Task<bool> TryRemoveAsync(Guid id)
    {
        if(id == Guid.Empty)
            throw new Exception("Guid is not a valid node id(empty)");
        
        var node = await db.Nodes.FirstOrDefaultAsync(n => n.Id == id);
        if (node == null) throw new Exception("Node not found");
        
        // if (node.Children.Count != 0)
        //     throw new Exception("You have to delete all children nodes first");

        db.Nodes.Remove(node);
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> TryRenameAsync(Guid id, string newName)
    {
        if(id == Guid.Empty)
            throw new Exception("Guid is not a valid node id(empty)");
        
        var node = await db.Nodes.FirstOrDefaultAsync(n => n.Id == id);
        if (node == null) throw new Exception("Node not found");

        // var siblings = await db.Nodes.Where(n => n.ParentId == node.ParentId && n.JournalId != node.Id).ToListAsync();
        // if (siblings.Any(s => s.Name == newName))
        //     throw new Exception("Node name must be unique among siblings");

        node.JournalHeader = newName;
        await db.SaveChangesAsync();
        return true;
    }
}