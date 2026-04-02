using KestrelsDev.KestrelsCore.Extensions;
using System.Text.Json;

namespace KestrelsDev.KestrelsCore.Tests.KestrelsCore.Extensions;

public class JsonExtensions_Tests
{
    [Test]
    public async Task ToJson__ConvertsObjectToJsonString()
    {
        TestObject obj = new() { Field1 = "test", Field2 = new() { Field = "test2" } };

        string manual = JsonSerializer.Serialize(obj);
        string toJson = obj.ToJson();

        await Assert.That(toJson).EqualTo(manual);
    }

    [Test]
    public async Task ToJson__UsesPassedSerializerOptions()
    {
        TestObject obj = new() { Field1 = "test", Field2 = new() { Field = "test2" } };
        JsonSerializerOptions options = new() { WriteIndented = true };

        string manual = JsonSerializer.Serialize(obj, options);
        string toJson = obj.ToJson(options);

        await Assert.That(toJson).EqualTo(manual);
    }

    [Test]
    public async Task CloneJson_CreatesCopyOfObject()
    {
        TestObject obj = new() { Field1 = "test", Field2 = new() { Field = "test2" } };

        TestObject cloned = obj.CloneJson().Value!;

        await Assert.That(cloned).IsNotEqualTo(obj);
        await Assert.That(cloned.Field1).EqualTo(obj.Field1);
        await Assert.That(cloned.Field2).IsNotEqualTo(obj.Field2);
        await Assert.That(cloned.Field2.Field).EqualTo(obj.Field2.Field);
    }

    private class TestObject
    {
        public required string Field1 { get; set; }
        public required NestedTestObject Field2 { get; set; }
    }

    private class NestedTestObject
    {
        public required string Field { get; set; }
    }
}
