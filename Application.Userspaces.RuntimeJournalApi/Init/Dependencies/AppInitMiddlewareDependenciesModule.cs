using Application.Middlewares.RuntimeJournal.Middlewares;
using Autofac;

namespace Application.Userspaces.RuntimeJournalApi.Init.Dependencies;

public class AppInitMiddlewareDependenciesModule: Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ExceptionHandlingMiddleware>();
    }
}