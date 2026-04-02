namespace KestrelsDev.KestrelsCore.DependencyInjection.Registration;

public class ServiceRegistration() : IServiceRegistration
{
    public ServiceRegister Registration => throw new NotImplementedException();

    public ServiceRegistration(ServiceRegistration other) : this() { }

    public void Add<TService>(TService instance)
    {
        throw new NotImplementedException();
    }

    public void Add<TService, TImpl>(TImpl instance) where TImpl : TService
    {
        throw new NotImplementedException();
    }

    public void Add<TService>(Func<IServiceScope, TService> factory, InjectionType injectionType = InjectionType.Transient)
    {
        throw new NotImplementedException();
    }

    public void Add<TService, TImpl>(Func<IServiceScope, TImpl> factory, InjectionType injectionType = InjectionType.Transient) where TImpl : TService
    {
        throw new NotImplementedException();
    }

    public void Add<TService>(InjectionType injectionType = InjectionType.Transient)
    {
        throw new NotImplementedException();
    }

    public void Add<TService, TImpl>(InjectionType injectionType = InjectionType.Transient) where TImpl : TService
    {
        throw new NotImplementedException();
    }

    public void AddKeyed<TService>(TService instance, object key)
    {
        throw new NotImplementedException();
    }

    public void AddKeyed<TService, TImpl>(TImpl instance, object key) where TImpl : TService
    {
        throw new NotImplementedException();
    }

    public void AddKeyed<TService>(Func<IServiceScope, TService> factory, object key, InjectionType injectionType = InjectionType.Transient)
    {
        throw new NotImplementedException();
    }

    public void AddKeyed<TService, TImpl>(Func<IServiceScope, TImpl> factory, object key, InjectionType injectionType = InjectionType.Transient) where TImpl : TService
    {
        throw new NotImplementedException();
    }

    public void AddKeyed<TService>(object key, InjectionType injectionType = InjectionType.Transient)
    {
        throw new NotImplementedException();
    }

    public void AddKeyed<TService, TImpl>(object key, InjectionType injectionType = InjectionType.Transient) where TImpl : TService
    {
        throw new NotImplementedException();
    }
}
