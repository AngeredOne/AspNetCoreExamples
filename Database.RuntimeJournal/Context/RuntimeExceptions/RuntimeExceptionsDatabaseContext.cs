using Database.Configuration;
using Database.RuntimeJournal.Models;
using Database.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Database.RuntimeJournal.Context;

public class RuntimeExceptionsDatabaseContext(DbContextOptions<RuntimeExceptionsDatabaseContext> options)  : DbContext(options)
{
    public DbSet<DbApplicationJournal> Nodes => Set<DbApplicationJournal>();
}