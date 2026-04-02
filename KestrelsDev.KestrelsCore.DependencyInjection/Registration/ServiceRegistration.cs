using KestrelsDev.KestrelsCore.DependencyInjection.Exceptions;
using System.Reflection;

namespace KestrelsDev.KestrelsCore.DependencyInjection.Registration;

public class ServiceRegistration() : IServiceRegistration
{
    public ServiceRegister Registration => new(_registration);
    private ServiceRegister _registration = [];

    public ServiceRegistration(ServiceRegistration other) : this() { }

    public void Add<TService>(TService instance) 
        where TService : class
        => AddInternal(typeof(TService), s => instance, InjectionType.Singleton);

    public void Add<TService, TImpl>(TImpl instance)
        where TImpl : TService
        where TService : class
        => AddInternal(typeof(TService), s => instance, InjectionType.Singleton);

    public void Add<TService>(Func<IServiceScope, TService> factory, InjectionType injectionType = InjectionType.Transient)
        where TService : class
        => AddInternal(typeof(TService), s => factory(s), injectionType);

    public void Add<TService, TImpl>(Func<IServiceScope, TImpl> factory, InjectionType injectionType = InjectionType.Transient)
        where TImpl : TService
        where TService : class
        => AddInternal(typeof(TService), s => factory(s), injectionType);

    public void Add<TService>(InjectionType injectionType = InjectionType.Transient)
        where TService : class
        => Add<TService, TService>(injectionType);

    public void Add<TService, TImpl>(InjectionType injectionType = InjectionType.Transient) 
        where TImpl : TService
        where TService : class
    {
        object factory(IServiceScope scope)
        {
            ConstructorInfo? ctor = typeof(TImpl).GetConstructors().FirstOrDefault();

            if (ctor is null)
                throw new NullInjectionException(typeof(TService), $"No public constructor found for registered type {typeof(TImpl)}");

            object[] args = ctor.GetParameters()
                .Select(p => scope.Get(p.GetType()))
                .ToArray();

            object constructed = ctor.Invoke(args);

            return constructed;
        }

        AddInternal(typeof(TService), factory, injectionType);
    }

    public void AddKeyed<TService>(TService instance, object key)
        where TService : class
    {
        throw new NotImplementedException();
    }

    public void AddKeyed<TService, TImpl>(TImpl instance, object key) 
        where TImpl : TService
        where TService : class
    {
        throw new NotImplementedException();
    }

    public void AddKeyed<TService>(Func<IServiceScope, TService> factory, object key, InjectionType injectionType = InjectionType.Transient)
        where TService : class
    {
        throw new NotImplementedException();
    }

    public void AddKeyed<TService, TImpl>(Func<IServiceScope, TImpl> factory, object key, InjectionType injectionType = InjectionType.Transient) 
        where TImpl : TService
        where TService : class
    {
        throw new NotImplementedException();
    }

    public void AddKeyed<TService>(object key, InjectionType injectionType = InjectionType.Transient)
        where TService : class
    {
        throw new NotImplementedException();
    }

    public void AddKeyed<TService, TImpl>(object key, InjectionType injectionType = InjectionType.Transient) 
        where TImpl : TService
        where TService : class
    {
        throw new NotImplementedException();
    }

    public RegisteredService? GetDefinition(Type type)
        => Registration.Get(type);

    public RegisteredService? GetKeyedDefinition(Type type, object key)
        => Registration.Get(type, key);

    private void AddInternal(Type serviceType, Func<IServiceScope, object> factory, InjectionType injectionType)
        => AddKeyedInternal(serviceType, factory, injectionType, string.Empty);

    private void AddKeyedInternal(Type serviceType, Func<IServiceScope, object> factory, InjectionType injectionType, object key)
    {
        RegisteredService service = new() { ServiceType = serviceType, Factory = factory, InjectionType = injectionType };
        _registration.Add(service, key);
    }
}
