using KestrelsDev.KestrelsCore.Configuration;
using KestrelsDev.KestrelsCore.Env;
using KestrelsDev.KestrelsCore.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace KestrelsDev.KestrelsCore.Web;

/// <summary>
/// Contains centralized methods for creating and configuring an ASP.NET Core <see cref="WebApplicationBuilder"/>
/// with options for application setup, logging, and default settings.
/// </summary>
public static class KestrelsCoreApplication
{
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// Creates a <see cref="WebApplicationBuilder"/> instance configured with specified options or defaults.
    /// </summary>
    /// <param name="options">
    /// An optional <see cref="WebApplicationOptions"/> object to configure the web application builder. If null,
    /// default options are used, which include settings like application name, content root path, and environment name.
    /// </param>
    /// <returns>
    /// A configured instance of <see cref="WebApplicationBuilder"/> with integrated logging and application settings.
    /// </returns>
    public static WebApplicationBuilder CreateBuilder(WebApplicationOptions? options = null)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(options ?? new());

        DotEnv.Load($"{builder.Environment.EnvironmentName}.env");
        DotEnv.Load(".env");

        builder.Configuration
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json",
                optional: true, reloadOnChange: false)
            .AddEnvironmentVariables();

        builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));

        CoreSettings coreSettings =
            builder.Configuration.GetSection(nameof(CoreSettings)).Get<CoreSettings.CoreSettingsModel>() ?? new();

        builder.Services.AddSingleton(coreSettings);

        return builder;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// Creates an instance of the <see cref="WebApplicationBuilder"/> configured with specific application
    /// settings or default options.
    /// </summary>
    /// <param name="args">
    /// Optional command-line arguments provided to the application. If null, default values are used.
    /// </param>
    /// <param name="applicationName">
    /// Optional application name. If null, the application name is derived from the entry assembly.
    /// </param>
    /// <param name="contentRootPath">
    /// Optional content root directory of the application. Specifies where the application's content files are located.
    /// A default value is used if not provided.
    /// </param>
    /// <param name="environmentName">
    /// Optional environment name, such as Development, Staging, or Production. If not specified, a default value is used.
    /// </param>
    /// <param name="webRootPath">
    /// Optional web root directory path for serving web assets. If not provided, defaults to "wwwroot".
    /// </param>
    /// <returns>
    /// A configured instance of <see cref="WebApplicationBuilder"/> ready for further customization or application setup.
    /// </returns>
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