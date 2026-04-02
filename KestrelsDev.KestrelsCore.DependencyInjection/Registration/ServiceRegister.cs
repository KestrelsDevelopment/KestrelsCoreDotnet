namespace KestrelsDev.KestrelsCore.DependencyInjection.Registration;

public class ServiceRegister : Dictionary<Type, ServiceRegister.ServiceKeyRegister>
{
    public ServiceRegister(ServiceRegister other)
    {
        foreach (KeyValuePair<Type, ServiceKeyRegister> kvp in other)
        {
            this[kvp.Key] = kvp.Value;
        }
    }

    public RegisteredService? Get(Type type)
        => Get(type, string.Empty);

    public RegisteredService? Get(Type type, object key)
    {
        if (TryGetValue(type, out var keyReg) && keyReg.TryGetValue(key, out var service))
            return service;

        return null;
    }

    public class ServiceKeyRegister : Dictionary<object, RegisteredService>
    {
        public ServiceKeyRegister(ServiceKeyRegister other)
        {
            foreach (KeyValuePair<object, RegisteredService> kvp in other)
            {
                this[kvp.Key] = kvp.Value;
            }
        }
    }
}
