using Microsoft.AspNetCore.Builder;

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
            return app;
        }
    }
}