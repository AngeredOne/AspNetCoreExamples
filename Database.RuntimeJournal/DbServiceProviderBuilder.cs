using Database.Configuration;
using Database.RuntimeJournal.Context;
using Database.RuntimeJournal.Repository;
using Database.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Repository.RuntimeJournal;

namespace Database.RuntimeJournal;

public static class DbServiceProviderBuilder
{
    public static ServiceProvider ConstructProvider()
    {
        IServiceCollection dbServices = new ServiceCollection();
        
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true).AddEnvironmentVariables()
            .Build();
        var dbConfig = configuration.GetSection("Database").GetSection("Postgres");
        var dbConfigValue = dbConfig.Get<PgsqlConfig>()!;
        dbServices.Configure<PgsqlConfig>(dbConfig);

        dbServices.AddDbContext<RuntimeExceptionsDatabaseContext>((sp, options) =>
        {
            var pg = sp.GetRequiredService<IOptions<PgsqlConfig>>().Value;
            options.UseNpgsql(pg.ToConnectionString());
        });

        dbServices.AddScoped<IRuntimeJournalRepository, JournalRepository>();

        var serviceProvider = dbServices.BuildServiceProvider();
        
        return serviceProvider;
    }
}