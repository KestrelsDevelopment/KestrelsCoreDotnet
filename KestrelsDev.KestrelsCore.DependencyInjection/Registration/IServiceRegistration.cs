namespace KestrelsDev.KestrelsCore.DependencyInjection.Registration;

public interface IServiceRegistration
{
    public ServiceRegister Registration { get; }
    public void Add<TService>(TService instance)
        where TService : class;
    public void Add<TService, TImpl>(TImpl instance) 
        where TImpl : TService 
        where TService : class;
    public void Add<TService>(Func<IServiceScope, TService> factory, InjectionType injectionType = InjectionType.Transient)
        where TService : class;
    public void Add<TService, TImpl>(Func<IServiceScope, TImpl> factory, InjectionType injectionType = InjectionType.Transient)
        where TImpl : TService
        where TService : class;
    public void Add<TService>(InjectionType injectionType = InjectionType.Transient)
        where TService : class;
    public void Add<TService, TImpl>(InjectionType injectionType = InjectionType.Transient)
        where TImpl : TService
        where TService : class;

    public void AddKeyed<TService>(TService instance, object key)
        where TService : class;
    public void AddKeyed<TService, TImpl>(TImpl instance, object key)
        where TImpl : TService
        where TService : class;
    public void AddKeyed<TService>(Func<IServiceScope, TService> factory, object key, InjectionType injectionType = InjectionType.Transient)
        where TService : class;
    public void AddKeyed<TService, TImpl>(Func<IServiceScope, TImpl> factory, object key, InjectionType injectionType = InjectionType.Transient)
        where TImpl : TService
        where TService : class;
    public void AddKeyed<TService>(object key, InjectionType injectionType = InjectionType.Transient)
        where TService : class;
    public void AddKeyed<TService, TImpl>(object key, InjectionType injectionType = InjectionType.Transient)
        where TImpl : TService
        where TService : class;

    public RegisteredService? GetDefinition(Type type);
    public RegisteredService? GetKeyedDefinition(Type type, object key);
}
