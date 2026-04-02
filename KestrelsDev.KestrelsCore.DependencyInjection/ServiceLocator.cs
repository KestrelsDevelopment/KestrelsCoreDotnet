using KestrelsDev.KestrelsCore.DependencyInjection.Registration;

namespace KestrelsDev.KestrelsCore.DependencyInjection;

public static class ServiceLocator
{
    public static readonly IServiceRegistration Registration = new ServiceRegistration();

    public static readonly IServiceScope DefaultScope = CreateScope();

    private static IServiceScope CreateScope() => new ServiceScope(Registration);
}