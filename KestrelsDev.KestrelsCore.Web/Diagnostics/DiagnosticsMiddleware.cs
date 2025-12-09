using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace KestrelsDev.KestrelsCore.Web.Diagnostics;

public class DiagnosticsMiddleware() : IMiddleware
{
    private const string DiagnosticsKey = "diagnostics_event";

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        using IServiceScope scope = context.RequestServices.CreateScope();
        IDiagnosticsService diagnosticsService = scope.ServiceProvider.GetRequiredService<IDiagnosticsService>();
        DiagnosticsEvent evt = diagnosticsService.StartTiming("Request");

        try
        {
            await next(context);
        }
        finally
        {
            evt.Finish();
        }
    }
}