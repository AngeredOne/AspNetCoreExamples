using ExceptionJournalApiExample.App.Middlewares;
using ExceptionJournalApiExample.Domain.Gateway;
using ExceptionJournalApiExample.Domain.Models.Core;
using Microsoft.EntityFrameworkCore;

namespace ExceptionJournalApiExample.Storage;

public class JournalRepository(AppDbContext db) : IJournalRepository
{
    public async Task<Node> TryCreateAsync(string uName)
    {
        var parent = await db.Nodes.Include(c => c.Children).FirstOrDefaultAsync(n => n.Name == uName);
        if (parent != null) throw new SecureException("Node name must be unique!");

        var createdId = Guid.NewGuid();
        
        var newNode = new Node
        {
            Id = createdId,
            Name = uName,
            ParentId = null,
            TreeId = createdId
        };

        db.Nodes.Add(newNode);
        await db.SaveChangesAsync();
        return newNode;
    }

    public async Task<Node> TryAttachAsync(Guid parentId, string uName)
    {
        if(parentId == Guid.Empty)
            throw new SecureException("Guid is not a valid node id(empty)");

        var parent = await db.Nodes.Include(p => p.Children).FirstOrDefaultAsync(n => n.Id == parentId);
        if (parent == null) throw new SecureException("Parent node not found");

        if (parent.Children.Any(c => c.Name == uName))
            throw new SecureException("Node name must be unique among siblings");

        var newNode = new Node
        {
            Name = uName,
            ParentId = parent.Id,
            TreeId = parent.TreeId
        };

        db.Nodes.Add(newNode);
        await db.SaveChangesAsync();
        
        return newNode;
    }

    public async Task<bool> TryRemoveAsync(Guid id)
    {
        if(id == Guid.Empty)
            throw new SecureException("Guid is not a valid node id(empty)");
        
        var node = await db.Nodes.Include(n => n.Children).FirstOrDefaultAsync(n => n.Id == id);
        if (node == null) throw new SecureException("Node not found");

        if (node.Children.Count != 0)
            throw new SecureException("You have to delete all children nodes first");

        db.Nodes.Remove(node);
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> TryRenameAsync(Guid id, string newName)
    {
        if(id == Guid.Empty)
            throw new SecureException("Guid is not a valid node id(empty)");
        
        var node = await db.Nodes.FirstOrDefaultAsync(n => n.Id == id);
        if (node == null) throw new SecureException("Node not found");

        var siblings = await db.Nodes.Where(n => n.ParentId == node.ParentId && n.Id != node.Id).ToListAsync();
        if (siblings.Any(s => s.Name == newName))
            throw new SecureException("Node name must be unique among siblings");

        node.Name = newName;
        await db.SaveChangesAsync();
        return true;
    }
}