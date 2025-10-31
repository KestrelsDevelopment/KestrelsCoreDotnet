// ReSharper disable UnusedMember.Global

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KestrelsDev.KestrelsCore.Web;

public static class ServiceCollectionExtensions
{

    #region AddHostedSingleton

    public static IServiceCollection AddHostedSingleton<TService>(
        this IServiceCollection services,
        Func<IServiceProvider, TService> factory) where TService : class, IHostedService
    {
        services.AddSingleton(factory);
        services.AddHostedService(p => p.GetRequiredService<TService>());

        return services;
    }

    public static IServiceCollection AddHostedSingleton<TService>(
        this IServiceCollection services) where TService : class, IHostedService
    {
        services.AddSingleton<TService>();
        services.AddHostedService(p => p.GetRequiredService<TService>());

        return services;
    }

    public static IServiceCollection AddHostedSingleton<TService>(
        this IServiceCollection services,
        TService instance) where TService : class, IHostedService
    {
        services.AddSingleton(instance);
        services.AddHostedService(p => p.GetRequiredService<TService>());

        return services;
    }

    public static IServiceCollection AddHostedSingleton<TService, TImpl>(
        this IServiceCollection services,
        Func<IServiceProvider, TImpl> factory) where TService : class, IHostedService where TImpl : class, TService
    {
        services.AddSingleton(factory);
        services.AddHostedService(p => p.GetRequiredService<TService>());

        return services;
    }

    public static IServiceCollection AddHostedSingleton<TService, TImpl>(
        this IServiceCollection services) where TService : class, IHostedService where TImpl : class, TService
    {
        services.AddSingleton<TService, TImpl>();
        services.AddHostedService(p => p.GetRequiredService<TService>());

        return services;
    }

    public static IServiceCollection AddHostedSingleton<TService, TImpl>(
        this IServiceCollection services,
        TImpl instance) where TService : class, IHostedService where TImpl : class, TService
    {
        services.AddSingleton(instance);
        services.AddHostedService(p => p.GetRequiredService<TService>());

        return services;
    }

    #endregion

}