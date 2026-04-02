namespace KestrelsDev.KestrelsCore.DependencyInjection.Registration;

/// <summary>
/// Represents the internal service register of an <see cref="IServiceRegistration"/>.
/// </summary>
public class ServiceRegister() : Dictionary<Type, ServiceRegister.ServiceKeyRegister>
{
    public ServiceRegister(ServiceRegister other) : this()
    {
        foreach (KeyValuePair<Type, ServiceKeyRegister> kvp in other)
        {
            this[kvp.Key] = new(kvp.Value);
        }
    }

    /// <summary>
    /// Retrieve the service definition of the service with a given type.
    /// </summary>
    /// <param name="type">The type of the retrieved service.</param>
    /// <returns>The definition of the service if it has been registered, or null otherwise.</returns>
    public RegisteredService? Get(Type type)
        => Get(type, string.Empty);

    /// <summary>
    /// Retrieve the service definition of the keyed service with a given type and key.
    /// </summary>
    /// <param name="type">The type of the retrieved service.</param>
    /// <param name="key">The key under which the service was saved.</param>
    /// <returns>The definition of the service if it has been registered, or null otherwise.</returns>
    public RegisteredService? Get(Type type, object key)
    {
        if (TryGetValue(type, out var keyReg) && keyReg.TryGetValue(key, out var service))
            return service;

        return null;
    }

    /// <summary>
    /// Adds a new service definition to the register.
    /// </summary>
    /// <param name="service">The service to register.</param>
    /// <param name="key">The key under which the service is registered.</param>
    public void Add(RegisteredService service, object key)
    {
        if (!TryGetValue(service.ServiceType, out ServiceKeyRegister? keyRegister))
        {
            keyRegister = [];
            this[service.ServiceType] = keyRegister;
        }

        keyRegister[key] = service;
    }

    public class ServiceKeyRegister() : Dictionary<object, RegisteredService>
    {
        public ServiceKeyRegister(ServiceKeyRegister other) : this()
        {
            foreach (KeyValuePair<object, RegisteredService> kvp in other)
            {
                this[kvp.Key] = kvp.Value;
            }
        }
    }
}
