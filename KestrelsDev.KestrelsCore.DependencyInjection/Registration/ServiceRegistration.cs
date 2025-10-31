using System.Collections.ObjectModel;

namespace KestrelsDev.KestrelsCore.DependencyInjection.Registration;

public class ServiceRegistration : IServiceRegistration
{

    public ReadOnlyDictionary<Type, object> Registrations { get; }

    public IServiceRegistration Add<TService, TImpl>() where TImpl : TService, new()
    {
        throw new NotImplementedException();
    }

    public IServiceRegistration Add<TService, TImpl>(TImpl singleton) where TImpl : TService
    {
        throw new NotImplementedException();
    }

    public IServiceRegistration Add<TService, TImpl>(Func<TImpl> factory) where TImpl : TService
    {
        throw new NotImplementedException();
    }
}