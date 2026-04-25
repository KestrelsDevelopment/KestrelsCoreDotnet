using KestrelsDev.KestrelsCore.DependencyInjection.Registration;

namespace KestrelsDev.KestrelsCore.Tests.KestrelsCore.DependencyInjection.Registration;

public class ServiceRegister_Tests
{
    private RegisteredService RandomService => new()
    {
        ServiceType = typeof(DummyType1),
        Factory = s => new { },
        InjectionType = InjectionType.Transient
    };

    [Test]
    public async Task Constructor__WithOther__CreatesCopy()
    {
        ServiceRegister original = [];
        original.Add(RandomService, 1);
        original.Add(RandomService, 2);
        original.Add(RandomService, 3);
        original[typeof(DummyType2)] = [];

        ServiceRegister clone = new(original);

        await Assert.That(clone).IsNotSameReferenceAs(original);
        await Assert.That(clone.Count).EqualTo(original.Count);
        await Assert.That(clone[typeof(DummyType1)]).IsNotSameReferenceAs(original[typeof(DummyType1)]);
        await Assert.That(clone[typeof(DummyType1)][1]).IsEqualTo(original[typeof(DummyType1)][1]);
        await Assert.That(clone[typeof(DummyType1)][2]).IsEqualTo(original[typeof(DummyType1)][2]);
        await Assert.That(clone[typeof(DummyType1)][3]).IsEqualTo(original[typeof(DummyType1)][3]);
    }

    [Test]
    public async Task Add__WithKey__AddsWithKey()
    {
        RegisteredService service = RandomService;
        object key = "key";
        ServiceRegister register = [];

        register.Add(service, key);

        await Assert.That(register[typeof(DummyType1)][key]).EqualTo(service);
    }

    [Test]
    public async Task Get__NotFound__ReturnsNull()
    {
        ServiceRegister register = [];

        RegisteredService? retrievedService = register.Get(typeof(DummyType1), "otherKey");

        await Assert.That(retrievedService).IsNull();
    }

    [Test]
    public async Task Get__Found_NoKey__ReturnsValueFromEmptyString()
    {
        RegisteredService service = RandomService;
        ServiceRegister register = [];

        register.Add(service, "");

        RegisteredService? retrievedService = register.Get(typeof(DummyType1));

        await Assert.That(retrievedService).EqualTo(service);
    }

    [Test]
    public async Task Get__Found_WithKey__ReturnsValueFromKey()
    {
        RegisteredService service = RandomService;
        object key = "key";
        ServiceRegister register = [];

        register.Add(service, key);

        RegisteredService? retrievedService = register.Get(typeof(DummyType1), key);

        await Assert.That(retrievedService).EqualTo(service);
    }

    [Test]
    public async Task Get__Found_WithDifferentKey__ReturnsNull()
    {
        RegisteredService service = RandomService;
        object key = "first key";
        ServiceRegister register = [];

        register.Add(service, key);

        RegisteredService? retrievedService = register.Get(typeof(DummyType1), "otherKey");

        await Assert.That(retrievedService).IsNull();
    }

    [Test]
    public async Task ServiceKeyRegister_Constructor__WithOther__CreatesCopy()
    {
        ServiceRegister.ServiceKeyRegister original = [];
        original[1] = RandomService;

        ServiceRegister.ServiceKeyRegister clone = new(original);

        await Assert.That(clone).IsNotSameReferenceAs(original);
        await Assert.That(clone[1]).IsEqualTo(original[1]);
        await Assert.That(clone.Count).IsEqualTo(original.Count);
    }

    private class DummyType1;
    private class DummyType2;
}
