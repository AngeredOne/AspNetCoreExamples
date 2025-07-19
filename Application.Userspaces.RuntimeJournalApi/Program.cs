using Autofac.Extensions.DependencyInjection;
using Application.Userspaces.RuntimeJournalApi.Init;
using Serilog;

Host.CreateDefaultBuilder(args)
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureAppConfiguration(cfg =>
    {
        cfg.AddEnvironmentVariables();
    })
    .UseSerilog((context, services, configuration) =>
    {
        configuration
            .ReadFrom.Configuration(context.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console();
    })
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
        webBuilder.ConfigureKestrel(serverOptions =>
        {
            serverOptions.ConfigureHttpsDefaults(httpsOptions =>
            {
                httpsOptions.ClientCertificateMode =
                    Microsoft.AspNetCore.Server.Kestrel.Https.ClientCertificateMode.NoCertificate;
                httpsOptions.AllowAnyClientCertificate();
            });
        });
    })
    .Build()
    .Run();