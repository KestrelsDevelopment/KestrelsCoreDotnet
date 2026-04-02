namespace KestrelsDev.KestrelsCore.DependencyInjection.Registration;

/// <summary>
/// Represents a container that holds definitions for injectable services.
/// </summary>
public interface IServiceRegistration
{
    /// <summary>
    /// A copy of the internal register used by this <see cref="IServiceRegistration"/>
    /// </summary>
    public ServiceRegister Register { get; }

    /// <summary>
    /// Adds a singleton instance of type <typeparamref name="TService"/> to the container.
    /// </summary>
    /// <typeparam name="TService">The type of the injectable service.</typeparam>
    /// <param name="instance">The instance to return when the service is injected.</param>
    public void Add<TService>(TService instance)
        where TService : class;

    /// <summary>
    /// Adds a singleton instance of type <typeparamref name="TService"/> to the container.
    /// </summary>
    /// <typeparam name="TService">The type of the injectable service.</typeparam>
    /// <typeparam name="TImpl">The type of the injected instance.</typeparam>
    /// <param name="instance">The instance to return when the service is injected.</param>
    public void Add<TService, TImpl>(TImpl instance) 
        where TImpl : TService 
        where TService : class;

    /// <summary>
    /// Adds a service of type <typeparamref name="TService"/> to the container that is constructed using the passed factory function.
    /// </summary>
    /// <typeparam name="TService">The type of the injectable service.</typeparam>
    /// <param name="factory">The factory function that is invoked when the service is injected.</param>
    /// <param name="injectionType">The <see cref="InjectionType"/> that this service is registered with.</param>
    public void Add<TService>(Func<IServiceScope, TService> factory, InjectionType injectionType = InjectionType.Transient)
        where TService : class;

    /// <summary>
    /// Adds a service of type <typeparamref name="TService"/> to the container that is constructed using the passed factory function.
    /// </summary>
    /// <typeparam name="TService">The type of the injectable service.</typeparam>
    /// <typeparam name="TImpl">The type of the injected instance.</typeparam>
    /// <param name="factory">The factory function that is invoked when the service is injected.</param>
    /// <param name="injectionType">The <see cref="InjectionType"/> that this service is registered with.</param>
    public void Add<TService, TImpl>(Func<IServiceScope, TImpl> factory, InjectionType injectionType = InjectionType.Transient)
        where TImpl : TService
        where TService : class;

    /// <summary>
    /// Adds a service of type <typeparamref name="TService"/> to the container that is constructed using 
    /// its first public constructor.
    /// </summary>
    /// <typeparam name="TService">The type of the injectable service.</typeparam>
    /// <param name="injectionType">The <see cref="InjectionType"/> that this service is registered with.</param>
    public void Add<TService>(InjectionType injectionType = InjectionType.Transient)
        where TService : class;

    /// <summary>
    /// Adds a service of type <typeparamref name="TService"/> to the container that is constructed using 
    /// the first public constructor of <typeparamref name="TImpl"/>.
    /// </summary>
    /// <typeparam name="TService">The type of the injectable service.</typeparam>
    /// <typeparam name="TImpl">The type of the injected.</typeparam>
    /// <param name="injectionType">The <see cref="InjectionType"/> that this service is registered with.</param>
    public void Add<TService, TImpl>(InjectionType injectionType = InjectionType.Transient)
        where TImpl : TService
        where TService : class;

    /// <summary>
    /// Adds a singleton instance of type <typeparamref name="TService"/> to the container, 
    /// which can be retrieved using a specified key.
    /// </summary>
    /// <typeparam name="TService">The type of the injectable service.</typeparam>
    /// <param name="instance">The instance to return when the service is injected.</param>
    /// <param name="key">The key used to retrieve the registered service.</param>
    public void AddKeyed<TService>(TService instance, object key)
        where TService : class;

    /// <summary>
    /// Adds a singleton instance of type <typeparamref name="TService"/> to the container, 
    /// which can be retrieved using a specified key.
    /// </summary>
    /// <typeparam name="TService">The type of the injectable service.</typeparam>
    /// <typeparam name="TImpl">The type of the injected instance.</typeparam>
    /// <param name="instance">The instance to return when the service is injected.</param>
    /// <param name="key">The key used to retrieve the registered service.</param>
    public void AddKeyed<TService, TImpl>(TImpl instance, object key)
        where TImpl : TService
        where TService : class;

    /// <summary>
    /// Adds a service of type <typeparamref name="TService"/> to the container that is constructed using 
    /// the passed factory function and which can be retrieved using a specified key.
    /// </summary>
    /// <typeparam name="TService">The type of the injectable service.</typeparam>
    /// <param name="factory">The factory function that is invoked when the service is injected.</param>
    /// <param name="injectionType">The <see cref="InjectionType"/> that this service is registered with.</param>
    /// <param name="key">The key used to retrieve the registered service.</param>
    public void AddKeyed<TService>(Func<IServiceScope, TService> factory, object key, InjectionType injectionType = InjectionType.Transient)
        where TService : class;

    /// <summary>
    /// Adds a service of type <typeparamref name="TService"/> to the container that is constructed using 
    /// the passed factory function and which can be retrieved using a specified key.
    /// </summary>
    /// <typeparam name="TService">The type of the injectable service.</typeparam>
    /// <typeparam name="TImpl">The type of the injected instance.</typeparam>
    /// <param name="factory">The factory function that is invoked when the service is injected.</param>
    /// <param name="injectionType">The <see cref="InjectionType"/> that this service is registered with.</param>
    /// <param name="key">The key used to retrieve the registered service.</param>
    public void AddKeyed<TService, TImpl>(Func<IServiceScope, TImpl> factory, object key, InjectionType injectionType = InjectionType.Transient)
        where TImpl : TService
        where TService : class;

    /// <summary>
    /// Adds a service of type <typeparamref name="TService"/> to the container that is constructed using 
    /// its first public constructor and which can be retrieved using a specified key.
    /// </summary>
    /// <typeparam name="TService">The type of the injectable service.</typeparam>
    /// <param name="injectionType">The <see cref="InjectionType"/> that this service is registered with.</param>
    /// <param name="key">The key used to retrieve the registered service.</param>
    public void AddKeyed<TService>(object key, InjectionType injectionType = InjectionType.Transient)
        where TService : class;

    /// <summary>
    /// Adds a service of type <typeparamref name="TService"/> to the container that is constructed using
    /// the first public constructor of <typeparamref name="TImpl"/> and which can be retrieved using a specified key.
    /// </summary>
    /// <typeparam name="TService">The type of the injectable service.</typeparam>
    /// <typeparam name="TImpl">The type of the injected.</typeparam>
    /// <param name="injectionType">The <see cref="InjectionType"/> that this service is registered with.</param>
    /// <param name="key">The key used to retrieve the registered service.</param>
    public void AddKeyed<TService, TImpl>(object key, InjectionType injectionType = InjectionType.Transient)
        where TImpl : TService
        where TService : class;

    public RegisteredService? GetDefinition(Type type);
    public RegisteredService? GetKeyedDefinition(Type type, object key);
}
