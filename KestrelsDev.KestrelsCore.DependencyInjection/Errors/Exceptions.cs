namespace KestrelsDev.KestrelsCore.DependencyInjection.Errors;

public class NullInjectionException(string message) : ApplicationException(message)
{
    public NullInjectionException(Type injectedType, string message) 
        : this($"Cannot inject {injectedType.Name}: {message}") { }
}

public class ServiceConstructionException(string message) : ApplicationException(message)
{
    public ServiceConstructionException(Type injectedType, string message)
        : this($"Cannot construct {injectedType.Name}: {message}") { }
}
