using Database.Configuration;
using Database.RuntimeJournal.Context;
using Database.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Database.RuntimeJournal;

public class ContextBuilderFactory : IDesignTimeDbContextFactory<RuntimeExceptionsDatabaseContext>
{
    public RuntimeExceptionsDatabaseContext CreateDbContext(string[] args)
    {
        using var scope = DbServiceProviderBuilder.ConstructProvider().CreateScope();
        var config = scope.ServiceProvider.GetRequiredService<IOptions<PgsqlConfig>>().Value;
        
        var builder = new DbContextOptionsBuilder<RuntimeExceptionsDatabaseContext>().UseNpgsql(config.ToConnectionString());
        return new RuntimeExceptionsDatabaseContext(builder.Options);
    }
}