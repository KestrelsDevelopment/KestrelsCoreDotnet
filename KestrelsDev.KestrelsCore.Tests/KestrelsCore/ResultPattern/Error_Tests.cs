using KestrelsDev.KestrelsCore.ResultPattern;

namespace KestrelsDev.KestrelsCore.Tests.KestrelsCore.ResultPattern;

public class Error_Tests
{
    [Test]
    public async Task Constructor__CalledWithMessage__ReturnsInstanceWithMessageAndNoExceptionOrPayload()
    {
        string message = "errorMessage";

        Error err = new(message);

        await Assert.That(err.Message).EqualTo(message);
        await Assert.That(err.Exception).IsNull();
        await Assert.That(err.Payload).IsNull();
    }

    [Test]
    public async Task Constructor__CalledWithMessageAndException__ReturnsInstanceWithMessageAndExceptionAndNoPayload()
    {
        string message = "errorMessage";
        Exception ex = new(message);

        Error err = new(message, ex);

        await Assert.That(err.Message).EqualTo(message);
        await Assert.That(err.Exception).EqualTo(ex);
        await Assert.That(err.Payload).IsNull();
    }

    [Test]
    public async Task Constructor__CalledWithMessageAndExceptionAndPayload__ReturnsInstanceWithMessageAndExceptionAndPayload()
    {
        string message = "errorMessage";
        Exception ex = new(message);
        object payload = new { };

        Error err = new(message, ex, payload);

        await Assert.That(err.Message).EqualTo(message);
        await Assert.That(err.Exception).EqualTo(ex);
        await Assert.That(err.Payload).EqualTo(payload);
    }

    [Test]
    public async Task Constructor__CalledWithMessageAndPayload__ReturnsInstanceWithMessageAndPayloadAndNoException()
    {
        string message = "errorMessage";
        object payload = new { };

        Error err = new(message, payload);

        await Assert.That(err.Message).EqualTo(message);
        await Assert.That(err.Exception).IsNull();
        await Assert.That(err.Payload).EqualTo(payload);
    }

    [Test]
    public async Task Conversion_FromString__CreatesErrorFromString()
    {
        string message = "errorMessage";

        Error err = message;

        await Assert.That(err.Message).EqualTo(message);
        await Assert.That(err.Exception).IsNull();
        await Assert.That(err.Payload).IsNull();
    }

    [Test]
    public async Task Conversion_FromException__CreatesErrorFromException()
    {
        string message = "errorMessage";
        Exception ex = new(message);

        Error err = ex;

        await Assert.That(err.Message).EqualTo(message);
        await Assert.That(err.Exception).EqualTo(ex);
        await Assert.That(err.Payload).IsNull();
    }

    [Test]
    public async Task Conversion_FromListOfError__CreatesAggregateError()
    {
        List<Error> errors = [new("first"), new("second")];

        Error err = errors;
        AggregateError? aggErr = err as AggregateError;

        await Assert.That(aggErr).IsNotNull();
        await Assert.That(aggErr!.Errors.Count).EqualTo(2);
        await Assert.That(aggErr!.Errors).Contains(errors[0]);
        await Assert.That(aggErr!.Errors).Contains(errors[1]);
    }

    [Test]
    public async Task Conversion_ToString__ReturnsMessage()
    {
        Error err = new("errorMessage");

        string converted = err;

        await Assert.That(converted).EqualTo(err.Message);
    }

    [Test]
    public async Task ToString__ReturnsMessage()
    {
        Error err = new("errorMessage");

        string converted = err.ToString();

        await Assert.That(converted).EqualTo(err.Message);
    }

    [Test]
    public async Task IsSimilarTo__SameErrorMessage__ReturnsTrue()
    {
        string errorMessage = "error";
        Error first = new(errorMessage);
        Error second = new(errorMessage);

        bool similar = first.IsSimilarTo(second);

        await Assert.That(similar).IsTrue();
    }

    [Test]
    public async Task IsSimilarTo__DifferentErrorMessage__ReturnsFalse()
    {
        Error first = new("1st");
        Error second = new("2nd");

        bool similar = first.IsSimilarTo(second);

        await Assert.That(similar).IsFalse();
    }
}
