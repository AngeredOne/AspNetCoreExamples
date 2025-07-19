using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Database.RuntimeJournal;

public class Program
{
    public static void Main(string[] args)
    {
        using var scope = DbServiceProviderBuilder.ConstructProvider().CreateScope();
        var context = new ContextBuilderFactory().CreateDbContext([]);
        context.Database.Migrate();
    }    
}