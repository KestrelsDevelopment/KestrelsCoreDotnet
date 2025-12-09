using KestrelsDev.KestrelsCore.Web.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace KestrelsDev.KestrelsCore.Web;

// ReSharper disable once UnusedType.Global
public static class WebApplicationExtensions
{
    extension(WebApplication app)
    {
        public WebApplication UseKestrelsCore()
        {
            app.UseDiagnostics();

            return app;
        }

        public WebApplication UseDiagnostics()
        {
            using IServiceScope scope = app.Services.CreateScope();
            _ = scope.ServiceProvider.GetRequiredService<IDiagnosticsService>();

            app.UseMiddleware<DiagnosticsMiddleware>();

            return app;
        }
    }
}