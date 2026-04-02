namespace KestrelsDev.KestrelsCore.DependencyInjection.Registration;



public interface IServiceRegistration
{
    public ServiceRegister Registration { get; }
    public void Add<TService>(TService instance);
    public void Add<TService, TImpl>(TImpl instance) where TImpl : TService;
    public void Add<TService>(Func<IServiceScope, TService> factory, InjectionType injectionType = InjectionType.Transient);
    public void Add<TService, TImpl>(Func<IServiceScope, TImpl> factory, InjectionType injectionType = InjectionType.Transient)
        where TImpl : TService;
    public void Add<TService>(InjectionType injectionType = InjectionType.Transient);
    public void Add<TService, TImpl>(InjectionType injectionType = InjectionType.Transient)
        where TImpl : TService;

    public void AddKeyed<TService>(TService instance, object key);
    public void AddKeyed<TService, TImpl>(TImpl instance, object key)
        where TImpl : TService;
    public void AddKeyed<TService>(Func<IServiceScope, TService> factory, object key, InjectionType injectionType = InjectionType.Transient);
    public void AddKeyed<TService, TImpl>(Func<IServiceScope, TImpl> factory, object key, InjectionType injectionType = InjectionType.Transient)
        where TImpl : TService;
    public void AddKeyed<TService>(object key, InjectionType injectionType = InjectionType.Transient);
    public void AddKeyed<TService, TImpl>(object key, InjectionType injectionType = InjectionType.Transient)
        where TImpl : TService;
}
