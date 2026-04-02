namespace KestrelsDev.KestrelsCore.Tests.KestrelsCore.DependencyInjection;

public class ServiceScope_Tests
{
    [Test]
    public async Task Validate__AllServicesConstructible__ReturnsSuccessResult()
    {
    }

    [Test]
    public async Task Validate__CannotConstructService__ReturnsAggregateErrorResult()
    {
    }

    [Test]
    public async Task Get__ServiceRegistered__ReturnsService()
    {
    }

    [Test]
    public async Task Get__ServiceNotRegistered__ThrowsException()
    {
    }

    [Test]
    public async Task TryGet__ServiceRegistered__ReturnsResultWithService()
    {
    }

    [Test]
    public async Task TryGet__ServiceNotRegistered__ReturnsResultWithError()
    {
    }

    [Test]
    public async Task Get__TransientService__NewInstanceEachTime()
    {
    }

    [Test]
    public async Task Get__ScopedService__NewInstanceForEachScope()
    {
    }

    [Test]
    public async Task Get__SingletonService__OneInstance()
    {
    }

    [Test]
    public async Task TryGet__TransientService__NewInstanceEachTime()
    {
    }

    [Test]
    public async Task TryGet__ScopedService__NewInstanceForEachScope()
    {
    }

    [Test]
    public async Task TryGet__SingletonService__OneInstancePerScopeTree()
    {
    }

    [Test]
    public async Task GetKeyed__ServiceRegistered__ReturnsService()
    {
    }

    [Test]
    public async Task GetKeyed__ServiceNotRegistered__ThrowsException()
    {
    }

    [Test]
    public async Task GetKeyed__ServiceRegisteredForDifferentKey__ThrowsException()
    {
    }

    [Test]
    public async Task TryGetKeyed__ServiceRegistered__ReturnsResultWithService()
    {
    }

    [Test]
    public async Task TryGetKeyed__ServiceNotRegistered__ReturnsResultWithError()
    {
    }

    [Test]
    public async Task TryGetKeyed__ServiceRegisteredForDifferentKey__ReturnsResultWithError()
    {
    }

    [Test]
    public async Task GetKeyed__TransientService__NewInstanceEachTime()
    {
    }

    [Test]
    public async Task GetKeyed__ScopedService__NewInstanceForEachScope()
    {
    }

    [Test]
    public async Task GetKeyed__SingletonService__OneInstance()
    {
    }

    [Test]
    public async Task TryGetKeyed__TransientService__NewInstanceEachTime()
    {
    }

    [Test]
    public async Task TryGetKeyed__ScopedService__NewInstanceForEachScope()
    {
    }

    [Test]
    public async Task TryGetKeyed__SingletonService__OneInstancePerScopeTree()
    {
    }

    [Test]
    public async Task CreateChildScope__CreatesScopeWithSameRegistration()
    {
    }
}
