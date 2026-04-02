using KestrelsDev.KestrelsCore.DependencyInjection;
using KestrelsDev.KestrelsCore.DependencyInjection.Registration;
using KestrelsDev.KestrelsCore.ResultPattern;

namespace KestrelsDev.KestrelsCore.Tests.KestrelsCore.DependencyInjection;

public class ServiceScope_Tests
{
#pragma warning disable CS9113 // Parameter is unread.
    private class ServiceWithDependency(Service service);
#pragma warning restore CS9113 // Parameter is unread.
    private class Service();

    private object Key => "key";

    [Test]
    public async Task Validate__AllServicesConstructible__ReturnsSuccessResult()
    {
        ServiceRegistration registration = new();
        ServiceScope scope = new(registration);
        registration.Add<ServiceWithDependency>();
        registration.Add<Service>();

        Result result = scope.Validate();

        await Assert.That(result.IsError).IsFalse();
    }

    [Test]
    public async Task Validate__CannotConstructService__ReturnsAggregateErrorResult()
    {
        ServiceRegistration registration = new();
        ServiceScope scope = new(registration);
        registration.Add<ServiceWithDependency>();

        Result result = scope.Validate();
        AggregateError? aggErr = result.Error as AggregateError;

        await Assert.That(result.IsError).IsTrue();
        Assert.NotNull(aggErr);
        await Assert.That(aggErr.Errors.Count).EqualTo(1);
    }

    [Test]
    public async Task Get__ServiceRegistered__ReturnsService()
    {
        ServiceRegistration registration = new();
        ServiceScope scope = new(registration);
        registration.Add<Service>();

        Service constructed = scope.Get<Service>();

        Assert.NotNull(constructed);
    }

    [Test]
    public async Task Get__ServiceNotRegistered__ThrowsException()
    {
        ServiceRegistration registration = new();
        ServiceScope scope = new(registration);

        Assert.Throws<Exception>(() => scope.Get<Service>());
    }

    [Test]
    public async Task TryGet__ServiceRegistered__ReturnsResultWithService()
    {
        ServiceRegistration registration = new();
        ServiceScope scope = new(registration);
        registration.Add<Service>();

        Result<Service> constructed = scope.TryGet<Service>();

        await Assert.That(constructed.HasValue).IsTrue();
    }

    [Test]
    public async Task TryGet__ServiceNotRegistered__ReturnsResultWithError()
    {
        ServiceRegistration registration = new();
        ServiceScope scope = new(registration);

        Result<Service> constructed = scope.TryGet<Service>();

        await Assert.That(constructed.IsError).IsTrue();
    }

    [Test]
    public async Task Get__TransientService__NewInstanceEachTime()
    {
        Assert.Fail("");
    }

    [Test]
    public async Task Get__ScopedService__NewInstanceForEachScope()
    {
        Assert.Fail("");
    }

    [Test]
    public async Task Get__SingletonService__OneInstance()
    {
        Assert.Fail("");
    }

    [Test]
    public async Task TryGet__TransientService__NewInstanceEachTime()
    {
        Assert.Fail("");
    }

    [Test]
    public async Task TryGet__ScopedService__NewInstanceForEachScope()
    {
        Assert.Fail("");
    }

    [Test]
    public async Task TryGet__SingletonService__OneInstancePerScopeTree()
    {
        Assert.Fail("");
    }

    [Test]
    public async Task GetKeyed__ServiceRegistered__ReturnsService()
    {
        ServiceRegistration registration = new();
        ServiceScope scope = new(registration);
        registration.AddKeyed<Service>(Key);

        Service constructed = scope.GetKeyed<Service>(Key);

        Assert.NotNull(constructed);
    }

    [Test]
    public async Task GetKeyed__ServiceNotRegistered__ThrowsException()
    {
        ServiceRegistration registration = new();
        ServiceScope scope = new(registration);

        Assert.Throws<Exception>(() => scope.GetKeyed<Service>(Key));
    }

    [Test]
    public async Task GetKeyed__ServiceRegisteredForDifferentKey__ThrowsException()
    {
        ServiceRegistration registration = new();
        ServiceScope scope = new(registration);
        registration.AddKeyed<Service>(Key);

        Assert.Throws<Exception>(() => scope.GetKeyed<Service>("other key"));
    }

    [Test]
    public async Task TryGetKeyed__ServiceRegistered__ReturnsResultWithService()
    {
        ServiceRegistration registration = new();
        ServiceScope scope = new(registration);
        registration.AddKeyed<Service>(Key);

        Result<Service> constructed = scope.TryGetKeyed<Service>(Key);

        await Assert.That(constructed.HasValue).IsTrue();
    }

    [Test]
    public async Task TryGetKeyed__ServiceNotRegistered__ReturnsResultWithError()
    {
        ServiceRegistration registration = new();
        ServiceScope scope = new(registration);

        Result<Service> constructed = scope.TryGetKeyed<Service>(Key);

        await Assert.That(constructed.IsError).IsTrue();
    }

    [Test]
    public async Task TryGetKeyed__ServiceRegisteredForDifferentKey__ReturnsResultWithError()
    {
        ServiceRegistration registration = new();
        ServiceScope scope = new(registration);
        registration.AddKeyed<Service>(Key);

        Result<Service> constructed = scope.TryGetKeyed<Service>("other key");

        await Assert.That(constructed.IsError).IsTrue();
    }

    [Test]
    public async Task GetKeyed__TransientService__NewInstanceEachTime()
    {
        Assert.Fail("");
    }

    [Test]
    public async Task GetKeyed__ScopedService__NewInstanceForEachScope()
    {
        Assert.Fail("");
    }

    [Test]
    public async Task GetKeyed__SingletonService__OneInstance()
    {
        Assert.Fail("");
    }

    [Test]
    public async Task TryGetKeyed__TransientService__NewInstanceEachTime()
    {
        Assert.Fail("");
    }

    [Test]
    public async Task TryGetKeyed__ScopedService__NewInstanceForEachScope()
    {
        Assert.Fail("");
    }

    [Test]
    public async Task TryGetKeyed__SingletonService__OneInstancePerScopeTree()
    {
        Assert.Fail("");
    }

    [Test]
    public async Task CreateChildScope__CreatesScopeWithSameRegistration()
    {
        Assert.Fail("");
    }
}
