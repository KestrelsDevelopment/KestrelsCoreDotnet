namespace KestrelsDev.KestrelsCore.DependencyInjection.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a dependency injection
/// operation is attempted on a null or invalid object.
/// </summary>
/// <remarks>
/// This exception is specific to the dependency injection
/// operations performed within the KestrelsCore framework.
/// It generally indicates an issue where a service is not properly
/// registered, or the resolution of a service fails due to invalid
/// configurations, such as missing constructors or incorrect mappings.
/// </remarks>
public class NullInjectionException(string message) : ApplicationException(message)
{

}