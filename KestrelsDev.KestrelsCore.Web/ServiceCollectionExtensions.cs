// ReSharper disable UnusedMember.Global

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KestrelsDev.KestrelsCore.Web;

/// <summary>
/// Provides extension methods for registering hosted singleton services within an IServiceCollection.
/// These methods simplify the process of adding implementations of IHostedService to the service
/// container, offering support for various registration patterns, such as using factory delegates,
/// specific instances, or implementing types.
/// </summary>
public static class ServiceCollectionExtensions
{

    #region AddHostedSingleton

    /// <summary>
    /// Registers a singleton service of the specified type <typeparamref name="TService"/> that implements <see cref="IHostedService"/>
    /// and ensures it is hosted by the application using a provided factory function.
    /// </summary>
    /// <typeparam name="TService">The type of the service to register. Must implement <see cref="IHostedService"/>.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <param name="factory">A factory function to create the service instance.</param>
    /// <returns>The <see cref="IServiceCollection"/> with the singleton and hosted service registered.</returns>
    public static IServiceCollection AddHostedSingleton<TService>(
        this IServiceCollection services,
        Func<IServiceProvider, TService> factory) where TService : class, IHostedService
    {
        services.AddSingleton(factory);
        services.AddHostedService(p => p.GetRequiredService<TService>());

        return services;
    }

    /// <summary>
    /// Registers a singleton service of type <typeparamref name="TService"/> that implements <see cref="IHostedService"/>
    /// and ensures it is hosted by the application.
    /// </summary>
    /// <typeparam name="TService">The type of the service to register. Must implement <see cref="IHostedService"/>.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <returns>The <see cref="IServiceCollection"/> with the singleton and hosted service registered.</returns>
    public static IServiceCollection AddHostedSingleton<TService>(
        this IServiceCollection services) where TService : class, IHostedService
    {
        services.AddSingleton<TService>();
        services.AddHostedService(p => p.GetRequiredService<TService>());

        return services;
    }

    /// <summary>
    /// Registers a singleton service of the specified type <typeparamref name="TService"/> that implements <see cref="IHostedService"/>
    /// and ensures it is hosted by the application by using the provided instance.
    /// </summary>
    /// <typeparam name="TService">The type of the service to register. Must implement <see cref="IHostedService"/>.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <param name="instance">The instance of <typeparamref name="TService"/> to register as a singleton and hosted service.</param>
    /// <returns>The <see cref="IServiceCollection"/> with the singleton and hosted service registered.</returns>
    public static IServiceCollection AddHostedSingleton<TService>(
        this IServiceCollection services,
        TService instance) where TService : class, IHostedService
    {
        services.AddSingleton(instance);
        services.AddHostedService(p => p.GetRequiredService<TService>());

        return services;
    }

    /// <summary>
    /// Registers a singleton service of the specified type <typeparamref name="TService"/> that implements <see cref="IHostedService"/>,
    /// and ensures it is hosted by the application, using a specified implementation type <typeparamref name="TImpl"/> and a provided factory function.
    /// </summary>
    /// <typeparam name="TService">The type of the service to register. Must implement <see cref="IHostedService"/>.</typeparam>
    /// <typeparam name="TImpl">The implementation type of the service. Must implement <typeparamref name="TService"/> and <see cref="IHostedService"/>.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <param name="factory">A factory function to create the service instance of type <typeparamref name="TImpl"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/> with the singleton and hosted service registered.</returns>
    public static IServiceCollection AddHostedSingleton<TService, TImpl>(
        this IServiceCollection services,
        Func<IServiceProvider, TImpl> factory) where TService : class, IHostedService where TImpl : class, TService
    {
        services.AddSingleton(factory);
        services.AddHostedService(p => p.GetRequiredService<TService>());

        return services;
    }

    /// <summary>
    /// Registers a hosted singleton service of the specified type <typeparamref name="TService"/>
    /// that implements <see cref="IHostedService"/> using the specified implementation type <typeparamref name="TImpl"/>.
    /// </summary>
    /// <typeparam name="TService">The type of the service to register. Must implement <see cref="IHostedService"/>.</typeparam>
    /// <typeparam name="TImpl">The type of the implementation to use for the service. Must implement <typeparamref name="TService"/>.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <returns>The <see cref="IServiceCollection"/> with the singleton and hosted service registered.</returns>
    public static IServiceCollection AddHostedSingleton<TService, TImpl>(
        this IServiceCollection services) where TService : class, IHostedService where TImpl : class, TService
    {
        services.AddSingleton<TService, TImpl>();
        services.AddHostedService(p => p.GetRequiredService<TService>());

        return services;
    }

    /// <summary>
    /// Registers a singleton service of the specified type <typeparamref name="TService"/> that implements <see cref="IHostedService"/>
    /// and ensures it is hosted by the application using a provided instance.
    /// </summary>
    /// <typeparam name="TService">The type of the service to register. Must implement <see cref="IHostedService"/>.</typeparam>
    /// <typeparam name="TImpl">The implementation type of the service, if different from <typeparamref name="TService"/>. Must implement <see cref="IHostedService"/>.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <param name="instance">An instance of the service to use for registration.</param>
    /// <returns>The <see cref="IServiceCollection"/> with the singleton and hosted service registered.</returns>
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