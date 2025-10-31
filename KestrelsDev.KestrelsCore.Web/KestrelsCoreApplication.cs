using KestrelsDev.KestrelsCore.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace KestrelsDev.KestrelsCore.Web;

public static class KestrelsCoreApplication
{
    // ReSharper disable once MemberCanBePrivate.Global
    public static WebApplicationBuilder CreateBuilder(WebApplicationOptions? options = null)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(options ?? new());

        builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));

        CoreSettings coreSettings =
            builder.Configuration.GetSection(nameof(CoreSettings)).Get<CoreSettings.CoreSettingsModel>() ?? new();

        builder.Services.AddSingleton(coreSettings);

        return builder;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public static WebApplicationBuilder CreateBuilder(
        string[]? args = null,
        string? applicationName = null,
        string? contentRootPath = null,
        string? environmentName = null,
        string? webRootPath = null)
        => CreateBuilder(new()
        {
            ApplicationName = applicationName,
            Args = args,
            ContentRootPath = contentRootPath,
            EnvironmentName = environmentName,
            WebRootPath = webRootPath
        });
}