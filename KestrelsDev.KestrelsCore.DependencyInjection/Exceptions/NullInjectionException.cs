namespace KestrelsDev.KestrelsCore.DependencyInjection.Exceptions;

public class NullInjectionException(Type constructedType, string message) : ApplicationException($"Cannot inject {constructedType.Name}: {message}");
