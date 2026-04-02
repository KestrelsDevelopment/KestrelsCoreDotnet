using KestrelsDev.KestrelsCore.DependencyInjection.Registration;
using KestrelsDev.KestrelsCore.ResultPattern;

namespace KestrelsDev.KestrelsCore.DependencyInjection;

public class ServiceScope(IServiceRegistration registration) : IServiceScope
{
    private IServiceScope? _parentScope;

    private ServiceScope(IServiceRegistration registration, IServiceScope parentScope) : this(registration)
    {
        _parentScope = parentScope;
    }

    public IServiceScope CreateChildScope()
    {
        throw new NotImplementedException();
    }

    public TService Get<TService>()
    {
        throw new NotImplementedException();
    }

    public object Get(Type serviceType)
    {
        throw new NotImplementedException();
    }

    public TService GetKeyed<TService>(object key)
    {
        throw new NotImplementedException();
    }

    public object GetKeyed(Type serviceType, object key)
    {
        throw new NotImplementedException();
    }

    public Result<TService> TryGet<TService>()
    {
        throw new NotImplementedException();
    }

    public Result<object> TryGet(Type serviceType)
    {
        throw new NotImplementedException();
    }

    public Result<TService> TryGetKeyed<TService>(object key)
    {
        throw new NotImplementedException();
    }

    public Result<object> TryGetKeyed(Type serviceType, object key)
    {
        throw new NotImplementedException();
    }

    public Result Validate()
    {
        throw new NotImplementedException();
    }
}