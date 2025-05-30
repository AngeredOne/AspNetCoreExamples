using ExceptionJournalApiExample.Domain.Models.Core;
using Microsoft.EntityFrameworkCore;

namespace ExceptionJournalApiExample.Storage;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Node> Nodes => Set<Node>();
    public DbSet<ExceptionJournal> ExceptionJournals => Set<ExceptionJournal>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Node>()
            .HasOne(n => n.Parent)
            .WithMany(p => p.Children)
            .HasForeignKey(n => n.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
}