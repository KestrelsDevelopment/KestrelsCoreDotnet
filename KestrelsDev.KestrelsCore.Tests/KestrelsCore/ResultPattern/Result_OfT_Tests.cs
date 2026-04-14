using KestrelsDev.KestrelsCore.ResultPattern;

namespace KestrelsDev.KestrelsCore.Tests.KestrelsCore.ResultPattern;

public class Result_OfT_Tests
{
    private Result<string> WithValue => new("someValue");
    private Result<string> WithError => new(new Error("someError"));

    private record MyError() : Error("testing error") { }

    [Test]
    public async Task Constructor__CalledWithValue__ReturnsInstanceWithValue()
    {
        string value = "someValue";

        Result<string> result = new(value);

        await Assert.That(result.HasValue).IsTrue();
        await Assert.That(result.IsError).IsFalse();
        await Assert.That(result.Value).EqualTo(value);
        await Assert.That<Error>(result.Error).IsNull();
    }

    [Test]
    public async Task Constructor__CalledWithError__ReturnsInstanceWithError()
    {
        Error error = new("someError");

        Result<string> result = new(error);

        await Assert.That(result.HasValue).IsFalse();
        await Assert.That(result.IsError).IsTrue();
        await Assert.That(result.Value).IsNull();
        await Assert.That<Error>(result.Error).EqualTo(error);
    }

    [Test]
    public async Task Then__InstanceHasValue_CalledWithAction__ExecutesAction_ReturnsInstance()
    {
        Result<string> result = WithValue;
        bool called = false;
        Action action = () => called = true;

        Result<string> thenResult = result.Then(action);

        await Assert.That(called).IsTrue();
        await Assert.That<Result<string>>(result).EqualTo(thenResult);
    }

    [Test]
    public async Task Then__InstanceHasValue_CalledWithActionOfT__ExecutesAction_ReturnsInstance()
    {
        string value = "someValue";
        Result<string> result = new(value);
        string? arg = null;
        Action<string> action = a => arg = a;

        Result<string> thenResult = result.Then(action);

        await Assert.That(arg).EqualTo(value);
        await Assert.That<Result<string>>(result).EqualTo(thenResult);
    }

    [Test]
    public async Task Then__InstanceHasError_CalledWithAction__DoesNotExecute_ReturnsInstance()
    {
        Result<string> result = WithError;
        bool called = false;
        Action action = () => called = true;

        Result<string> thenResult = result.Then(action);

        await Assert.That(called).IsFalse();
        await Assert.That<Result<string>>(result).EqualTo(thenResult);
    }

    [Test]
    public async Task Then__InstanceHasError_CalledWithActionOfT__DoesNotExecute_ReturnsInstance()
    {
        Result<string> result = WithError;
        string? arg = null;
        Action<string> action = a => arg = a;

        Result<string> thenResult = result.Then(action);

        await Assert.That(arg).IsNull();
        await Assert.That<Result<string>>(result).EqualTo(thenResult);
    }

    [Test]
    public async Task Catch_AnyError__InstanceHasError__ExecutesActionWithError_ReturnsInstance()
    {
        Error err = new("someError", new Exception());
        Result<string> result = new(err);
        Error? calledWith = null;

        Result<string> catchResult = result.Catch((Error errArg) => calledWith = errArg);

        await Assert.That<Error>(calledWith).EqualTo(err);
        await Assert.That<Result<string>>(catchResult).EqualTo(result);
    }

    [Test]
    public async Task Catch_SpecificError__InstanceHasMatchingError__ExecutesActionWithException_ReturnsInstance()
    {
        MyError err = new();
        Result<string> result = err;
        Error? calledWith = null;

        Result<string> catchResult = result.Catch<MyError>(errArg => calledWith = errArg);

        await Assert.That<Error>(calledWith).EqualTo(err);
        await Assert.That<Result<string>>(catchResult).EqualTo(result);
    }

    [Test]
    public async Task Catch_SpecificException__InstanceHasErrorWithMatchingException__ExecutesActionWithError_ReturnsInstance()
    {
        Error err = new("someError", new ArgumentException());
        Result<string> result = new(err);
        Error? calledWith = null;

        Result<string> catchResult = result.Catch<ArgumentException>((Error errArg) => calledWith = errArg);

        await Assert.That<Error>(calledWith).EqualTo(err);
        await Assert.That<Result<string>>(catchResult).EqualTo(result);
    }

    [Test]
    public async Task Catch_SpecificError__InstanceHasDifferentError__DoesNotExecuteAction_ReturnsInstance()
    {
        Result<string> result = WithError;
        Error? calledWith = null;

        Result<string> catchResult = result.Catch<NullReferenceException>(errArg => calledWith = errArg);

        await Assert.That<Error>(calledWith).IsNull();
        await Assert.That<Result<string>>(catchResult).EqualTo(result);
    }

    [Test]
    public async Task Catch_SpecificException__InstanceHasErrorWithDifferentException__DoesNotExecuteAction_ReturnsInstance()
    {
        Error err = new("someError", new ArgumentException());
        Result<string> result = new(err);
        Error? calledWith = null;

        Result<string> catchResult = result.Catch<NullReferenceException>((Error errArg) => calledWith = errArg);

        await Assert.That<Error>(calledWith).IsNull();
        await Assert.That<Result<string>>(catchResult).EqualTo(result);
    }

    [Test]
    public async Task Catch_AnyError__InstanceHasErrorWithoutException__ExecutesActionWithError_ReturnsInstance()
    {
        Result<string> result = WithError;
        Error? calledWith = null;

        Result<string> catchResult = result.Catch((Error errArg) => calledWith = errArg);

        await Assert.That<Error>(calledWith).EqualTo(result.Error);
        await Assert.That<Result<string>>(catchResult).EqualTo(result);
    }

    [Test]
    public async Task Catch_SpecificError__InstanceHasMatchingErrorWithoutException__ExecutesActionWithError_ReturnsInstance()
    {
        MyError err = new();
        Result<string> result = err;
        Error? calledWith = null;

        Result<string> catchResult = result.Catch<MyError>(errArg => calledWith = errArg);

        await Assert.That<Error>(calledWith).EqualTo(err);
        await Assert.That<Result<string>>(catchResult).EqualTo(result);
    }

    [Test]
    public async Task Catch_SpecificException__InstanceHasErrorWithoutException__DoesNotExecuteAction_ReturnsInstance()
    {
        Result<string> result = WithError;
        Error? calledWith = null;

        Result<string> catchResult = result.Catch<ArgumentException>((Error errArg) => calledWith = errArg);

        await Assert.That<Error>(calledWith).IsNull();
        await Assert.That<Result<string>>(catchResult).EqualTo(result);
    }

    [Test]
    public async Task Catch_AnyError__InstanceHasValue__DoesNotExecuteAction_ReturnsInstance()
    {
        Result<string> result = WithValue;
        Error? calledWith = null;

        Result<string> catchResult = result.Catch((Error errArg) => calledWith = errArg);

        await Assert.That<Error>(calledWith).IsNull();
        await Assert.That<Result<string>>(catchResult).EqualTo(result);
    }

    [Test]
    public async Task Catch_SpecificError__InstanceHasValue__DoesNotExecuteAction_ReturnsInstance()
    {
        Result<string> result = WithValue;
        Error? calledWith = null;

        Result<string> catchResult = result.Catch<MyError>(errArg => calledWith = errArg);

        await Assert.That<Error>(calledWith).IsNull();
        await Assert.That<Result<string>>(catchResult).EqualTo(result);
    }

    [Test]
    public async Task Catch_SpecificException__InstanceHasValue__DoesNotExecuteAction_ReturnsInstance()
    {
        Result<string> result = WithValue;
        Error? calledWith = null;

        Result<string> catchResult = result.Catch<ArgumentException>((Error errArg) => calledWith = errArg);

        await Assert.That<Error>(calledWith).IsNull();
        await Assert.That<Result<string>>(catchResult).EqualTo(result);
    }

    [Test]
    public async Task Throw_AnyException__InstanceHasError__ThrowsException()
    {
        Result<string> result = new(new Error("someError", new Exception()));

        Exception thrown = Assert.Throws<Exception>(() => result.Throw());
        await Assert.That(thrown).EqualTo(result.Error!.Exception);
    }

    [Test]
    public async Task Throw_SpecificException__InstanceHasErrorWithMatchingException__ThrowsException()
    {
        Result<string> result = new(new Error("someError", new ArgumentException()));

        Exception thrown = Assert.Throws<Exception>(() => result.Throw<ArgumentException>());
        await Assert.That(thrown).EqualTo(result.Error!.Exception);
    }

    [Test]
    public async Task Throw_SpecificException__InstanceHasErrorWithDifferentException__DoesNotThrow_ReturnsInstance()
    {
        Result<string> result = new(new Error("someError", new ArgumentException()));

        Result<string> throwResult = result.Throw<NullReferenceException>();

        await Assert.That<Result<string>>(throwResult).EqualTo(result);
    }

    [Test]
    public async Task Throw_AnyException__InstanceHasValue__DoesNotThrow_ReturnsInstance()
    {
        Result<string> result = WithValue;

        Result<string> throwResult = result.Throw();

        await Assert.That<Result<string>>(throwResult).EqualTo(result);
    }

    [Test]
    public async Task Throw_SpecificException__InstanceHasValue__DoesNotThrow_ReturnsInstance()
    {
        Result<string> result = WithValue;

        Result<string> throwResult = result.Throw<ArgumentException>();

        await Assert.That<Result<string>>(throwResult).EqualTo(result);
    }

    [Test]
    public async Task Throw_AnyException__InstanceHasErrorWithoutException__ThrowsExceptionFromError()
    {
        Result<string> result = WithError;

        Exception thrown = Assert.Throws<Exception>(() => result.Throw());
        await Assert.That(thrown.Message).EqualTo(result.Error!.Message);
    }

    [Test]
    public async Task Throw_SpecificException__InstanceHasErrorWithoutException__DoesNotThrow_ReturnsInstance()
    {
        Result<string> result = WithError;

        Result<string> throwResult = result.Throw<ArgumentException>();

        await Assert.That<Result<string>>(throwResult).EqualTo(result);
    }

    [Test]
    public async Task Or__InstanceHasValue__ReturnsValue()
    {
        Result<string> result = WithValue;

        string returnedValue = result.Or("fallback");

        await Assert.That(returnedValue).EqualTo(result.Value);
    }

    [Test]
    public async Task Or__InstanceHasError__ReturnsFallback()
    {
        Result<string> result = WithError;
        string fallback = "fallback";

        string returnedValue = result.Or(fallback);

        await Assert.That(returnedValue).EqualTo(fallback);
    }

    [Test]
    public async Task Map__InstanceHasValue__CallsValueFuncWithValue_DoesNotCallErrorFunc_ReturnsFuncResult()
    {
        Result<string> result = WithValue;
        string funcResult = "result";
        string? valueCalledWith = null;
        Error? errCalledWith = null;

        string returnedValue = result.Map(value =>
        {
            valueCalledWith = value;
            return funcResult;
        }, err =>
        {
            errCalledWith = err;
            return "";
        });

        await Assert.That(valueCalledWith).EqualTo(result.Value);
        await Assert.That<Error>(errCalledWith).IsNull();
        await Assert.That(returnedValue).EqualTo(funcResult);
    }

    [Test]
    public async Task Map__InstanceHasError__CallsErrorFuncWithError_DoesNotCallValueFunc_ReturnsFuncResult()
    {
        Result<string> result = WithError;
        string funcResult = "result";
        string? valueCalledWith = null;
        Error? errCalledWith = null;


        string returnedValue = result.Map(value =>
        {
            valueCalledWith = value;
            return "";
        }, err =>
        {
            errCalledWith = err;
            return funcResult;
        });

        await Assert.That<Error>(errCalledWith).EqualTo(result.Error);
        await Assert.That(valueCalledWith).IsNull();
        await Assert.That(returnedValue).EqualTo(funcResult);
    }

    [Test]
    public async Task Deconstruct__InstanceHasValue__ReturnsValueAndNull()
    {
        Result<string> result = WithValue;

        (string? value, Error? error) = result;

        await Assert.That(value).EqualTo(result.Value);
        await Assert.That<Error>(error).IsNull();
    }

    [Test]
    public async Task Deconstruct__InstanceHasError__ReturnsErrorAndNull()
    {
        Result<string> result = WithError;

        (string? value, Error? error) = result;

        await Assert.That(value).IsNull();
        await Assert.That<Error>(error).EqualTo(result.Error);
    }

    [Test]
    public async Task Conversion_FromError__ReturnsInstanceWithError()
    {
        Error err = new("someError");
        Result<string> result = err;

        await Assert.That(result.IsError).IsTrue();
        await Assert.That<Error>(result.Error).EqualTo(err);
    }

    [Test]
    public async Task Conversion_FromValue__ReturnsInstanceWithValue()
    {
        string value = "someValue";
        Result<string> result = value;

        await Assert.That(result.IsError).IsFalse();
        await Assert.That(result.Value).EqualTo(value);
    }

    [Test]
    public async Task Conversion_ToValue__InstanceHasValue__ReturnsValue()
    {
        Result<string> result = WithValue;

        string? value = result;

        await Assert.That(value).EqualTo(result.Value);
    }

    [Test]
    public async Task Conversion_ToValue__InstanceHasError__ReturnsNull()
    {
        Result<string> result = WithError;

        string? value = result;

        await Assert.That(value).IsNull();
    }
}
