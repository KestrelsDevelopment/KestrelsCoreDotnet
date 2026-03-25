using KestrelsDev.KestrelsCore.ResultPattern;

namespace KestrelsDev.KestrelsCore.Tests.KestrelsCore.ResultPattern;

public class ResultTests
{
    private Result<string> WithValue => new("someValue");
    private Result<string> WithError => new(new Error("someError"));

    [Test]
    public async Task Result_T__CalledWithValue__ReturnsInstanceWithValue()
    {
        string value = "someValue";

        Result<string> result = new(value);

        await Assert.That(result.HasValue).IsTrue();
        await Assert.That(result.IsError).IsFalse();
        await Assert.That(result.Value).EqualTo(value);
        await Assert.That<Error>(result.Error).IsNull();
    }

    [Test]
    public async Task Result_T__CalledWithError__ReturnsInstanceWithError()
    {
        Error error = new("someError");

        Result<string> result = new(error);

        await Assert.That(result.HasValue).IsFalse();
        await Assert.That(result.IsError).IsTrue();
        await Assert.That(result.Value).IsNull();
        await Assert.That<Error>(result.Error).EqualTo(error);
    }

    [Test]
    public async Task Then__InstanceHasValue_CalledWithAction__ExecutesAction()
    {
        Result<string> result = WithValue;
        bool called = false;
        Action action = () => called = true;

        result.Then(action);

        await Assert.That(called).IsTrue();
    }

    [Test]
    public async Task Then__InstanceHasValue_CalledWithAction_T__ExecutesAction()
    {
        string value = "someValue";
        Result<string> result = new(value);
        string? arg = null;
        Action<string> action = a => arg = a;

        result.Then(action);

        await Assert.That(arg).EqualTo(value);
    }
}
