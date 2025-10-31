using System.Collections.ObjectModel;

namespace KestrelsDev.KestrelsCore.DependencyInjection.Registration;

public interface IServiceRegistration
{
    public ReadOnlyDictionary<Type, object> Registrations { get; }

    public IServiceRegistration Add<TService, TImpl>() where TImpl : TService, new();

    public IServiceRegistration Add<TService>() where TService : new();

    public IServiceRegistration Add<TService, TImpl>(TImpl singleton) where TImpl : TService;

    public IServiceRegistration Add<TService>(TService singleton);

    public IServiceRegistration Add<TService, TImpl>(Func<TImpl> factory) where TImpl : TService;

    public IServiceRegistration Add<TService>(Func<TService> factory);
}