using KestrelsDev.KestrelsCore.DependencyInjection.Errors;
using System.Reflection;

namespace KestrelsDev.KestrelsCore.DependencyInjection.Registration;

public class ServiceRegistration() : IServiceRegistration
{
    public ServiceRegister Register => new(_register);
    private readonly ServiceRegister _register = [];

    public ServiceRegistration(ServiceRegistration other) : this()
    {
        _register = other._register;
    }

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
        => AddKeyed<TService, TImpl>(ServiceRegister.EmptyKey, injectionType);

    public void AddKeyed<TService>(TService instance, object key)
        where TService : class
        => AddKeyedInternal(typeof(TService), s => instance, InjectionType.Singleton, key);

    public void AddKeyed<TService, TImpl>(TImpl instance, object key)
        where TImpl : TService
        where TService : class
        => AddKeyedInternal(typeof(TService), s => instance, InjectionType.Singleton, key);

    public void AddKeyed<TService>(Func<IServiceScope, TService> factory, object key, InjectionType injectionType = InjectionType.Transient)
        where TService : class
        => AddKeyedInternal(typeof(TService), s => factory(s), injectionType, key);

    public void AddKeyed<TService, TImpl>(Func<IServiceScope, TImpl> factory, object key, InjectionType injectionType = InjectionType.Transient)
        where TImpl : TService
        where TService : class
        => AddKeyedInternal(typeof(TService), s => factory(s), injectionType, key);

    public void AddKeyed<TService>(object key, InjectionType injectionType = InjectionType.Transient)
        where TService : class
        => AddKeyed<TService, TService>(key, injectionType);

    public void AddKeyed<TService, TImpl>(object key, InjectionType injectionType = InjectionType.Transient)
        where TImpl : TService
        where TService : class
    {
        object factory(IServiceScope scope)
        {
            ConstructorInfo? ctor = typeof(TImpl).GetConstructors().FirstOrDefault();

            if (ctor is null)
                throw new ServiceConstructionException(typeof(TService), $"No public constructor found for registered type {typeof(TImpl)}");

            object[] args = ctor.GetParameters()
                .Select(p => scope.Get(p.ParameterType))
                .ToArray();

            object constructed = ctor.Invoke(args);

            if(!constructed.GetType().IsAssignableTo(typeof(TService)))
                throw new ServiceConstructionException(typeof(TService), "Constructed object is of unexpected type");

            return constructed;
        }

        AddKeyedInternal(typeof(TService), factory, injectionType, key);
    }

    public RegisteredService? GetDefinition(Type type)
        => Register.Get(type);

    public RegisteredService? GetKeyedDefinition(Type type, object key)
        => Register.Get(type, key);

    private void AddInternal(Type serviceType, Func<IServiceScope, object> factory, InjectionType injectionType)
        => AddKeyedInternal(serviceType, factory, injectionType, ServiceRegister.EmptyKey);

    private void AddKeyedInternal(Type serviceType, Func<IServiceScope, object> factory, InjectionType injectionType, object key)
    {
        RegisteredService service = new() { ServiceType = serviceType, Factory = factory, InjectionType = injectionType };
        _register.Add(service, key);
    }
}
