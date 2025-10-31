using KestrelsDev.KestrelsCore.ResultPattern;

namespace KestrelsDev.KestrelsCore.DependencyInjection;

public interface IServiceScope
{
    public TService New<TService>();

    public TService Singleton<TService>();

    public Result Validate();
}