using KestrelsDev.KestrelsCore.ResultPattern;

namespace KestrelsDev.KestrelsCore.DependencyInjection;

public interface IServiceScope
{
    public TService Get<TService>();
    public Result<TService> TryGet<TService>();
    public TService GetKeyed<TService>(object key);
    public Result<TService> TryGetKeyed<TService>(object key);
    public object Get(Type serviceType);
    public Result<object> TryGet(Type serviceType);
    public object GetKeyed(Type serviceType, object key);
    public Result<object> TryGetKeyed(Type serviceType, object key);
    public Result Validate();
    public IServiceScope CreateChildScope();
}