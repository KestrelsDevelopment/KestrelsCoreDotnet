using KestrelsDev.KestrelsCore.DependencyInjection.Registration;

namespace KestrelsDev.KestrelsCore.DependencyInjection;

public static class ServiceLocator
{
    public static readonly IServiceRegistration Registration = new ServiceRegistration();

    public static readonly IServiceScope DefaultScope = CreateScope();

    public static IServiceScope CreateScope() => new ServiceScope(Registration);

    public static TService New<TService>() => DefaultScope.New<TService>();

    public static TService Singleton<TService>() => DefaultScope.Singleton<TService>();
}