using System.Collections.ObjectModel;

namespace KestrelsDev.KestrelsCore.DependencyInjection.Registration;

public class ServiceRegistration : IServiceRegistration
{
    public ReadOnlyDictionary<Type, object> Registrations => new(RegistrationsInternal);

    private readonly Dictionary<Type, object> RegistrationsInternal = [];

    public IServiceRegistration Add<TService, TImpl>() where TImpl : TService, new()
        => AddInternal<TService>(typeof(TImpl));

    public IServiceRegistration Add<TService>() where TService : new()
        => AddInternal<TService>(typeof(TService));

    public IServiceRegistration Add<TService, TImpl>(TImpl singleton) where TImpl : TService
        => AddInternal<TService>(singleton);

    public IServiceRegistration Add<TService>(TService singleton)
        => AddInternal<TService>(singleton);

    public IServiceRegistration Add<TService, TImpl>(Func<TImpl> factory) where TImpl : TService
        => AddInternal<TService>(factory);

    public IServiceRegistration Add<TService>(Func<TService> factory)
        => AddInternal<TService>(factory);

    private ServiceRegistration AddInternal<TService>(object? value)
    {
        RegistrationsInternal[typeof(TService)] = value ?? throw new ArgumentNullException(nameof(value));

        return this;
    }
}