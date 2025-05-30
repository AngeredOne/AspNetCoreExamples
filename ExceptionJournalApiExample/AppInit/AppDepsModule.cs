using Autofac;
using ExceptionJournalApiExample.App.Middlewares;

namespace ExceptionJournalApiExample.AppInit;

public class AppDepsModule: Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ExceptionHandlingMiddleware>();
    }
}