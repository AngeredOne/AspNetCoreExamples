using System.Text.Json.Serialization;
using Asp.Versioning;
using Autofac;
using ExceptionJournalApiExample.App.Middlewares;
using ExceptionJournalApiExample.Domain.Gateway;
using ExceptionJournalApiExample.Domain.Models.Configuration;
using ExceptionJournalApiExample.Storage;
using ExceptionJournalApiExample.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ExceptionJournalApiExample.AppInit;

public class Startup(IConfiguration configuration)
{
    private IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<PgsqlConfig>(Configuration.GetSection("DB"));
        
        services.AddControllers().AddJsonOptions(o =>
        {
            o.JsonSerializerOptions.ReferenceHandler =
                ReferenceHandler.IgnoreCycles;
        });
        
        services.AddApiVersioning(opt =>
        {
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.ReportApiVersions = true;
            opt.ApiVersionReader = new UrlSegmentApiVersionReader();
        })
        .AddMvc()
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });;
        
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            var pg = sp.GetRequiredService<IOptions<PgsqlConfig>>().Value;
            options.UseNpgsql(pg.ToConnectionString());
        });

        services.AddAutoMapper(typeof(CoreToApiMapping));
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule<AppDepsModule>();
        builder.RegisterType<JournalRepository>().As<IJournalRepository>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.NewVersionedApi();
        });
    }
}