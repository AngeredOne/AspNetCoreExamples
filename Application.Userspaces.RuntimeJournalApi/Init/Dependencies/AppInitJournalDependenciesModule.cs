using Application.Backends.RuntimeJournal.Interfaces;
using Application.Backends.RuntimeJournal.Services;
using Application.Middlewares.RuntimeJournal.Middlewares;
using Autofac;
using Database.RuntimeJournal.Repository;
using Repository.RuntimeJournal;
using RuntimeJournalServices.Interfaces;
using RuntimeJournalServices.Services;

namespace Application.Userspaces.RuntimeJournalApi.Init.Dependencies;

public class AppInitJournalDependenciesModule: Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<JournalRepository>().As<IRuntimeJournalRepository>();
        builder.RegisterType<JournalConstructionService>().As<IJournalConstructionService>();
        builder.RegisterType<CommonRuntimeJournalService>().As<ICommonRuntimeJournalService>();
        builder.RegisterType<ExceptionRuntimeJournalService>().As<IExceptionRuntimeJournalService>();
    }
}