using KestrelsDev.KestrelsCore.DependencyInjection;
using KestrelsDev.KestrelsCore.DependencyInjection.Registration;

namespace KestrelsDev.KestrelsCore.Tests.KestrelsCore.DependencyInjection.Registration;

public class ServiceRegistration_Tests
{
#pragma warning disable CS9113 // Parameter is unread.
    private class ServiceWithDependency(IService service) : IServiceWithDependency;
#pragma warning restore CS9113 // Parameter is unread.
    private interface IServiceWithDependency;
    private class Service() : IService;
    private interface IService;
    private ServiceScope Scope => new(new ServiceRegistration());
    private Service Instance { get; } = new();
    private Service InitialInstance { get; } = new();
    private object Key => "";
    private Type IFaceType => typeof(IService);
    private Type ImplType => typeof(Service);

    [Test]
    public async Task Constructor__WithOther__CreatesClone()
    {
    }

    [Test]
    public async Task GetDefinition__NotFound__ReturnsNull()
    {
        ServiceRegistration registration = new();

        RegisteredService? registered = registration.GetDefinition(ImplType);

        Assert.Null(registered);
    }

    [Test]
    public async Task GetDefinition__Found__ReturnsValueFromEmptyString()
    {
        ServiceRegistration registration = new();
        registration.Add(new Service());

        RegisteredService? registered = registration.GetDefinition(ImplType);
        RegisteredService? keyedRegistered = registration.GetKeyedDefinition(ImplType, "");

        Assert.NotNull(registered);
        await Assert.That(registered).EqualTo(keyedRegistered);
    }

    [Test]
    public async Task GetKeyedDefinition__Found__ReturnsValueFromKey()
    {
        ServiceRegistration registration = new();
        registration.AddKeyed(new Service(), Key);

        RegisteredService? registered = registration.GetKeyedDefinition(ImplType, Key);

        Assert.NotNull(registered);
    }

    [Test]
    public async Task GetKeyedDefinition__Found_WithDifferentKey__ReturnsNull()
    {
        ServiceRegistration registration = new();
        registration.AddKeyed(new Service(), Key);

        RegisteredService? registered = registration.GetKeyedDefinition(ImplType, "otherKey");

        Assert.Null(registered);
    }

    [Test]
    public async Task Add__SingletonInstance__RegistersFactoryThatReturnsInstance_OverridesExistingRegistration()
    {
        ServiceRegistration registration = new();
        registration.Add(InitialInstance);

        registration.Add(Instance);

        RegisteredService? registered = registration.GetDefinition(ImplType);
        object? created = registered?.Factory(Scope);

        await Assert.That(created).IsSameReferenceAs(Instance);
        await Assert.That(registered?.InjectionType).EqualTo(InjectionType.Singleton);
        await Assert.That(created).IsNotSameReferenceAs(InitialInstance);
    }

    [Test]
    public async Task Add__SingletonInstanceForInterface__RegistersFactoryThatReturnsInstance_OverridesExistingRegistration()
    {
        ServiceRegistration registration = new();
        registration.Add<IService>(InitialInstance);

        registration.Add<IService>(Instance);

        RegisteredService? registered = registration.GetDefinition(IFaceType);
        object? created = registered?.Factory(Scope);

        await Assert.That(created).IsSameReferenceAs(Instance);
        await Assert.That(registered?.InjectionType).EqualTo(InjectionType.Singleton);
        await Assert.That(created).IsNotSameReferenceAs(InitialInstance);
    }

    [Test]
    [Arguments(InjectionType.Transient)]
    [Arguments(InjectionType.Scoped)]
    [Arguments(InjectionType.Singleton)]
    public async Task Add__FactoryFunc__RegistersFactoryFunc_UsesPassedInjectionType_OverridesExistingRegistration(InjectionType injectionType)
    {
        ServiceRegistration registration = new();
        registration.Add(InitialInstance);

        registration.Add(s => Instance, injectionType);

        RegisteredService? registered = registration.GetDefinition(ImplType);
        object? created = registered?.Factory(Scope);

        await Assert.That(created).IsSameReferenceAs(Instance);
        await Assert.That(registered?.InjectionType).EqualTo(injectionType);
        await Assert.That(created).IsNotSameReferenceAs(InitialInstance);
    }

    [Test]
    [Arguments(InjectionType.Transient)]
    [Arguments(InjectionType.Scoped)]
    [Arguments(InjectionType.Singleton)]
    public async Task Add__FactoryFuncForInterface__RegistersFactoryFunc_UsesPassedInjectionType_OverridesExistingRegistration(InjectionType injectionType)
    {
        ServiceRegistration registration = new();
        registration.Add<IService>(InitialInstance);

        registration.Add<IService>(s => Instance, injectionType);

        RegisteredService? registered = registration.GetDefinition(IFaceType);
        object? created = registered?.Factory(Scope);

        await Assert.That(created).IsSameReferenceAs(Instance);
        await Assert.That(registered?.InjectionType).EqualTo(injectionType);
        await Assert.That(created).IsNotSameReferenceAs(InitialInstance);
    }

    [Test]
    [Arguments(InjectionType.Transient)]
    [Arguments(InjectionType.Scoped)]
    [Arguments(InjectionType.Singleton)]
    public async Task Add__ImplType__RegistersFactoryThatConstructsType_UsesPassedInjectionType_OverridesExistingRegistration(InjectionType injectionType)
    {
        ServiceRegistration registration = new();
        registration.Add(InitialInstance);

        registration.Add<Service>(injectionType);

        RegisteredService? registered = registration.GetDefinition(ImplType);
        object? created = registered?.Factory(Scope);

        await Assert.That(created).IsOfType(ImplType);
        await Assert.That(registered?.InjectionType).EqualTo(injectionType);
        await Assert.That(created).IsNotSameReferenceAs(InitialInstance);
    }

    [Test]
    [Arguments(InjectionType.Transient)]
    [Arguments(InjectionType.Scoped)]
    [Arguments(InjectionType.Singleton)]
    public async Task Add__ImplTypeForInterface__RegistersFactoryThatConstructsType_UsesPassedInjectionType_OverridesExistingRegistration(InjectionType injectionType)
    {
        ServiceRegistration registration = new();
        registration.Add<IService>(InitialInstance);

        registration.Add<IService, Service>(injectionType);

        RegisteredService? registered = registration.GetDefinition(IFaceType);
        object? created = registered?.Factory(Scope);

        await Assert.That(created).IsOfType(ImplType);
        await Assert.That(registered?.InjectionType).EqualTo(injectionType);
        await Assert.That(created).IsNotSameReferenceAs(InitialInstance);
    }

    [Test]
    [Arguments(InjectionType.Transient)]
    [Arguments(InjectionType.Scoped)]
    [Arguments(InjectionType.Singleton)]
    public async Task AddKeyed__SingletonInstance__RegistersFactoryThatReturnsInstance_OverridesExistingRegistration(InjectionType injectionType)
    {
        ServiceRegistration registration = new();
        registration.AddKeyed(InitialInstance, Key);

        registration.AddKeyed(Instance, Key);

        RegisteredService? registered = registration.GetKeyedDefinition(ImplType, Key);
        object? created = registered?.Factory(Scope);

        await Assert.That(created).IsSameReferenceAs(Instance);
        await Assert.That(registered?.InjectionType).EqualTo(InjectionType.Singleton);
        await Assert.That(created).IsNotSameReferenceAs(InitialInstance);
    }

    [Test]
    [Arguments(InjectionType.Transient)]
    [Arguments(InjectionType.Scoped)]
    [Arguments(InjectionType.Singleton)]
    public async Task AddKeyed__SingletonInstanceForInterface__RegistersFactoryThatReturnsInstance_OverridesExistingRegistration(InjectionType injectionType)
    {
        ServiceRegistration registration = new();
        registration.AddKeyed<IService>(InitialInstance, Key);

        registration.AddKeyed<IService>(Instance, Key);

        RegisteredService? registered = registration.GetKeyedDefinition(IFaceType, Key);
        object? created = registered?.Factory(Scope);

        await Assert.That(created).IsSameReferenceAs(Instance);
        await Assert.That(registered?.InjectionType).EqualTo(InjectionType.Singleton);
        await Assert.That(created).IsNotSameReferenceAs(InitialInstance);
    }

    [Test]
    [Arguments(InjectionType.Transient)]
    [Arguments(InjectionType.Scoped)]
    [Arguments(InjectionType.Singleton)]
    public async Task AddKeyed__FactoryFunc__RegistersFactoryFunc_UsesPassedInjectionType_OverridesExistingRegistration(InjectionType injectionType)
    {
        ServiceRegistration registration = new();
        registration.AddKeyed(InitialInstance, Key);

        registration.AddKeyed(s => Instance, Key);

        RegisteredService? registered = registration.GetKeyedDefinition(ImplType, Key);
        object? created = registered?.Factory(Scope);

        await Assert.That(created).IsSameReferenceAs(Instance);
        await Assert.That(registered?.InjectionType).EqualTo(InjectionType.Singleton);
        await Assert.That(created).IsNotSameReferenceAs(InitialInstance);
    }

    [Test]
    [Arguments(InjectionType.Transient)]
    [Arguments(InjectionType.Scoped)]
    [Arguments(InjectionType.Singleton)]
    public async Task AddKeyed__FactoryFuncForInterface__RegistersFactoryFunc_UsesPassedInjectionType_OverridesExistingRegistration(InjectionType injectionType)
    {
        ServiceRegistration registration = new();
        registration.AddKeyed<IService>(InitialInstance, Key);

        registration.AddKeyed<IService>(s => Instance, Key);

        RegisteredService? registered = registration.GetKeyedDefinition(IFaceType, Key);
        object? created = registered?.Factory(Scope);

        await Assert.That(created).IsSameReferenceAs(Instance);
        await Assert.That(registered?.InjectionType).EqualTo(InjectionType.Singleton);
        await Assert.That(created).IsNotSameReferenceAs(InitialInstance);
    }

    [Test]
    [Arguments(InjectionType.Transient)]
    [Arguments(InjectionType.Scoped)]
    [Arguments(InjectionType.Singleton)]
    public async Task AddKeyed__ImplType__RegistersFactoryThatConstructsType_UsesPassedInjectionType_OverridesExistingRegistration(InjectionType injectionType)
    {
        ServiceRegistration registration = new();
        registration.AddKeyed(InitialInstance, Key);

        registration.AddKeyed<Service>(Key, injectionType);

        RegisteredService? registered = registration.GetKeyedDefinition(ImplType, Key);
        object? created = registered?.Factory(Scope);

        await Assert.That(created).IsOfType(ImplType);
        await Assert.That(registered?.InjectionType).EqualTo(injectionType);
        await Assert.That(created).IsNotSameReferenceAs(InitialInstance);
    }

    [Test]
    [Arguments(InjectionType.Transient)]
    [Arguments(InjectionType.Scoped)]
    [Arguments(InjectionType.Singleton)]
    public async Task AddKeyed__ImplTypeForInterface__RegistersFactoryThatConstructsType_UsesPassedInjectionType_OverridesExistingRegistration(InjectionType injectionType)
    {
        ServiceRegistration registration = new();
        registration.AddKeyed<IService>(InitialInstance, Key);

        registration.AddKeyed<IService, Service>(Key, injectionType);

        RegisteredService? registered = registration.GetKeyedDefinition(IFaceType, Key);
        object? created = registered?.Factory(Scope);

        await Assert.That(created).IsOfType(ImplType);
        await Assert.That(registered?.InjectionType).EqualTo(injectionType);
        await Assert.That(created).IsNotSameReferenceAs(InitialInstance);
    }
}
