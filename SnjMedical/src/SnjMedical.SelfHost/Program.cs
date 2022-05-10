using Serilog;
using Newtonsoft.Json.Converters;
using SnjMedical.Application;
using SnjMedical.Infrastructure;
using SnjMedical.SelfHost.Features.Options;
using SnjMedical.SelfHost.Features.Swagger;
using SnjMedical.SelfHost.Features.Filters;
using SnjMedical.SelfHost.Features.AutoMapper;
using SnjMedical.SelfHost.Features.ApiVersioning;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using SnjMedical.SelfHost.Features.Middlewares;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var options = new ProjectOptions(
    configuration.GetValue<string>($"{nameof(ProjectOptions)}:{nameof(ProjectOptions.InstanceName)}"),
    configuration.GetValue<string>($"{nameof(ProjectOptions)}:{nameof(ProjectOptions.WebAddress)}")
    );

try
{
    builder.WebHost.UseKestrel();
    builder.WebHost.UseUrls(options.WebAddress);
    Log.Information("Configuring web host ({ApplicationName})...", options.InstanceName);
    builder.Services.AddCors();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(configuration);
    builder.Services.AddControllers(options =>
    {
        options.Filters.Add<HttpGlobalExceptionFilter>();
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    });
    builder.Services.AddSwagger();
    builder.Services.AddVersioning();
    builder.Services.AddAutoMapper();

    var app = builder.Build();
    var provider = builder.Services.BuildServiceProvider();
    app.UseSwagger(provider.GetRequiredService<IApiVersionDescriptionProvider>());
    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseMiddleware<RequestGuidMiddleware>();
    app.MapControllers();
    Log.Information("Starting web host ({ApplicationName})...", options.InstanceName);
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationName})!", options.InstanceName);
}
finally
{
    Log.CloseAndFlush();
}

