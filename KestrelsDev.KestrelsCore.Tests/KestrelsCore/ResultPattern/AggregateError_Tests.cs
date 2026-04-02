using KestrelsDev.KestrelsCore.ResultPattern;

namespace KestrelsDev.KestrelsCore.Tests.KestrelsCore.ResultPattern;

public class AggregateError_Tests
{
    [Test]
    public async Task Constructor__CalledWithMessageAndErrorList__ReturnsNewInstanceWithoutException()
    {
        List<Error> errors = [new("first"), new("second")];
        string message = "errorMessage";

        AggregateError err = new(message, errors);

        await Assert.That(err.Message).EqualTo(message);
        await Assert.That(err.Exception).IsNull();
        await Assert.That(err.Payload).EqualTo(errors);
        await Assert.That(err.Errors).EqualTo(errors);
    }

    [Test]
    public async Task Constructor__CalledWithMessageAndErrorListAndException__ReturnsNewInstanceWithException()
    {
        List<Error> errors = [new("first"), new("second")];
        string message = "errorMessage";
        Exception ex = new(message);

        AggregateError err = new(message, errors, ex);

        await Assert.That(err.Message).EqualTo(message);
        await Assert.That(err.Exception).EqualTo(ex);
        await Assert.That(err.Payload).EqualTo(errors);
        await Assert.That(err.Errors).EqualTo(errors);
    }

    [Test]
    public async Task Conversion_FromListOfError__CreatesAggregateError()
    {
        List<Error> errors = [new("first"), new("second")];

        AggregateError err = errors;

        await Assert.That(err.Errors).EqualTo(errors);
    }
}
