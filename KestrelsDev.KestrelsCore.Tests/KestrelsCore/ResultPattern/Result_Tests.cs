using KestrelsDev.KestrelsCore.ResultPattern;

namespace KestrelsDev.KestrelsCore.Tests.KestrelsCore.ResultPattern;

public class Result_Tests
{
    private Result WithSuccess => new(null);
    private Result WithError => new(new Error("error message"));

    [Test]
    public async Task Constructor__CalledWithError__ReturnsInstanceWithError()
    {
        Error error = new("someError");

        Result result = new(error);

        await Assert.That(result.IsError).IsTrue();
        await Assert.That<Error>(result.Error).EqualTo(error);
    }

    [Test]
    public async Task Constructor__CalledWithNull__ReturnsInstanceWithSuccess()
    {
        Result result = new(null);

        await Assert.That(result.IsError).IsFalse();
        await Assert.That<Error>(result.Error).IsNull();
    }

    [Test]
    public async Task Then__InstanceIsSuccess__ExecutesAction_ReturnsInstance()
    {
        Result result = WithSuccess;
        bool called = false;
        Action action = () => called = true;

        Result thenResult = result.Then(action);

        await Assert.That(called).IsTrue();
        await Assert.That(result).EqualTo(thenResult);
    }

    [Test]
    public async Task Then__InstanceIsError__DoesNotExecuteAction_ReturnsInstance()
    {
        Result result = WithError;
        bool called = false;
        Action action = () => called = true;

        Result thenResult = result.Then(action);

        await Assert.That(called).IsFalse();
        await Assert.That(result).EqualTo(thenResult);
    }

    [Test]
    public async Task Catch__InstanceHasError_AnyException_CalledWithActionOfException__ExecutesActionWithException_ReturnsInstance()
    {
        Exception ex = new();
        Result result = new(new Error("someError", ex));
        Exception? calledWith = null;

        Result catchResult = result.Catch(exArg => calledWith = exArg);

        await Assert.That(calledWith).EqualTo(ex);
        await Assert.That(catchResult).EqualTo(result);
    }

    [Test]
    public async Task Catch__InstanceHasError_AnyException_CalledWithActionOfError__ExecutesActionWithError_ReturnsInstance()
    {
        Error err = new("someError", new Exception());
        Result result = new(err);
        Error? calledWith = null;

        Result catchResult = result.Catch((Error errArg) => calledWith = errArg);

        await Assert.That<Error>(calledWith).EqualTo(err);
        await Assert.That(catchResult).EqualTo(result);
    }

    [Test]
    public async Task Catch__InstanceHasErrorWithMatchingException_SpecificException_CalledWithActionOfException__ExecutesActionWithException_ReturnsInstance()
    {
        ArgumentException ex = new();
        Result result = new(new Error("someError", ex));
        Exception? calledWith = null;

        Result catchResult = result.Catch<ArgumentException>(exArg => calledWith = exArg);

        await Assert.That(calledWith).EqualTo(ex);
        await Assert.That(catchResult).EqualTo(result);
    }

    [Test]
    public async Task Catch__InstanceHasErrorWithMatchingException_SpecificException_CalledWithActionOfError__ExecutesActionWithError_ReturnsInstance()
    {
        Error err = new("someError", new ArgumentException());
        Result result = new(err);
        Error? calledWith = null;

        Result catchResult = result.Catch<ArgumentException>((Error errArg) => calledWith = errArg);

        await Assert.That<Error>(calledWith).EqualTo(err);
        await Assert.That(catchResult).EqualTo(result);
    }

    [Test]
    public async Task Catch__InstanceHasErrorWithDifferentException_SpecificException_CalledWithActionOfException__DoesNotExecuteAction_ReturnsInstance()
    {
        ArgumentException ex = new();
        Result result = new(new Error("someError", ex));
        Exception? calledWith = null;

        Result catchResult = result.Catch<NullReferenceException>(exArg => calledWith = exArg);

        await Assert.That(calledWith).IsNull();
        await Assert.That(catchResult).EqualTo(result);
    }

    [Test]
    public async Task Catch__InstanceHasErrorWithDifferentException_SpecificException_CalledWithActionOfError__DoesNotExecuteAction_ReturnsInstance()
    {
        Error err = new("someError", new ArgumentException());
        Result result = new(err);
        Error? calledWith = null;

        Result catchResult = result.Catch<NullReferenceException>((Error errArg) => calledWith = errArg);

        await Assert.That<Error>(calledWith).IsNull();
        await Assert.That(catchResult).EqualTo(result);
    }

    [Test]
    public async Task Catch__InstanceHasErrorWithoutException_AnyException_CalledWithActionOfException__ExecutesActionWithExceptionFromError_ReturnsInstance()
    {
        Result result = WithError;
        Exception? calledWith = null;

        Result catchResult = result.Catch(exArg => calledWith = exArg);

        await Assert.That(calledWith).IsNotNull();
        await Assert.That(calledWith.Message).EqualTo(result.Error!.Message);
        await Assert.That(catchResult).EqualTo(result);
    }

    [Test]
    public async Task Catch__InstanceHasErrorWithoutException_AnyException_CalledWithActionOfError__ExecutesActionWithError_ReturnsInstance()
    {
        Result result = WithError;
        Error? calledWith = null;

        Result catchResult = result.Catch((Error errArg) => calledWith = errArg);

        await Assert.That<Error>(calledWith).EqualTo(result.Error);
        await Assert.That(catchResult).EqualTo(result);
    }

    [Test]
    public async Task Catch__InstanceHasErrorWithoutException_SpecificException_CalledWithActionOfException__DoesNotExecuteAction_ReturnsInstance()
    {
        Result result = WithError;
        Exception? calledWith = null;

        Result catchResult = result.Catch<ArgumentException>(exArg => calledWith = exArg);

        await Assert.That(calledWith).IsNull();
        await Assert.That(catchResult).EqualTo(result);
    }

    [Test]
    public async Task Catch__InstanceHasErrorWithoutException_SpecificException_CalledWithActionOfError__DoesNotExecuteAction_ReturnsInstance()
    {
        Result result = WithError;
        Error? calledWith = null;

        Result catchResult = result.Catch<ArgumentException>((Error errArg) => calledWith = errArg);

        await Assert.That<Error>(calledWith).IsNull();
        await Assert.That(catchResult).EqualTo(result);
    }

    [Test]
    public async Task Catch__InstanceIsSuccess_AnyException_CalledWithActionOfException__DoesNotExecuteAction_ReturnsInstance()
    {
        Result result = WithSuccess;
        Exception? calledWith = null;

        Result catchResult = result.Catch(exArg => calledWith = exArg);

        await Assert.That(calledWith).IsNull();
        await Assert.That(catchResult).EqualTo(result);
    }

    [Test]
    public async Task Catch__InstanceIsSuccess_AnyException_CalledWithActionOfError__DoesNotExecuteAction_ReturnsInstance()
    {
        Result result = WithSuccess;
        Error? calledWith = null;

        Result catchResult = result.Catch((Error errArg) => calledWith = errArg);

        await Assert.That<Error>(calledWith).IsNull();
        await Assert.That(catchResult).EqualTo(result);
    }

    [Test]
    public async Task Catch__InstanceIsSuccess_SpecificException_CalledWithActionOfException__DoesNotExecuteAction_ReturnsInstance()
    {
        Result result = WithSuccess;
        Exception? calledWith = null;

        Result catchResult = result.Catch<ArgumentException>(exArg => calledWith = exArg);

        await Assert.That(calledWith).IsNull();
        await Assert.That(catchResult).EqualTo(result);
    }

    [Test]
    public async Task Catch__InstanceIsSuccess_SpecificException_CalledWithActionOfError__DoesNotExecuteAction_ReturnsInstance()
    {
        Result result = WithSuccess;
        Error? calledWith = null;

        Result catchResult = result.Catch<ArgumentException>((Error errArg) => calledWith = errArg);

        await Assert.That<Error>(calledWith).IsNull();
        await Assert.That(catchResult).EqualTo(result);
    }

    [Test]
    public async Task Throw__InstanceHasError_AnyException__ThrowsException()
    {
        Result result = new(new Error("someError", new Exception()));

        Exception thrown = Assert.Throws<Exception>(() => result.Throw());

        await Assert.That(thrown).EqualTo(result.Error!.Exception);
    }

    [Test]
    public async Task Throw__InstanceHasErrorWithMatchingException_SpecificException__ThrowsException()
    {
        Result result = new(new Error("someError", new ArgumentException()));

        Exception thrown = Assert.Throws<Exception>(() => result.Throw<ArgumentException>());

        await Assert.That(thrown).EqualTo(result.Error!.Exception);
    }

    [Test]
    public async Task Throw__InstanceHasErrorWithDifferentException_SpecificException__DoesNotThrow_ReturnsInstance()
    {
        Result result = new(new Error("someError", new ArgumentException()));

        Result throwResult = result.Throw<NullReferenceException>();

        await Assert.That(throwResult).EqualTo(result);
    }

    [Test]
    public async Task Throw__InstanceIsSuccess_AnyException__DoesNotThrow_ReturnsInstance()
    {
        Result result = WithSuccess;

        Result throwResult = result.Throw();

        await Assert.That(throwResult).EqualTo(result);
    }

    [Test]
    public async Task Throw__InstanceIsSuccess_SpecificException__DoesNotThrow_ReturnsInstance()
    {
        Result result = WithSuccess;

        Result throwResult = result.Throw<ArgumentException>();

        await Assert.That(throwResult).EqualTo(result);
    }

    [Test]
    public async Task Throw__InstanceHasErrorWithoutException_AnyException__ThrowsExceptionFromError()
    {
        Result result = WithError;

        Exception thrown = Assert.Throws<Exception>(() => result.Throw());
        await Assert.That(thrown.Message).EqualTo(result.Error!.Message);
    }

    [Test]
    public async Task Throw__InstanceHasErrorWithoutException_SpecificException__DoesNotThrow_ReturnsInstance()
    {
        Result result = WithError;

        Result throwResult = result.Throw<ArgumentException>();

        await Assert.That(throwResult).EqualTo(result);
    }

    [Test]
    public async Task Map__InstanceIsSuccess__CallsSuccessFunc_DoesNotCallErrorFunc_ReturnsFuncResult()
    {
        Result result = WithSuccess;
        string funcResult = "result";
        bool successCalled = false;
        Error? errCalledWith = null;

        string returnedValue = result.Map(() =>
        {
            successCalled = true;
            return funcResult;
        }, err =>
        {
            errCalledWith = err;
            return "";
        });

        await Assert.That<Error>(errCalledWith).IsNull();
        await Assert.That(successCalled).IsTrue();
        await Assert.That(returnedValue).EqualTo(funcResult);
    }

    [Test]
    public async Task Map__InstanceHasError__CallsErrorFuncWithError_DoesNotCallSuccessFunc_ReturnsFuncResult()
    {
        Result result = WithError;
        string funcResult = "result";
        bool successCalled = false;
        Error? errCalledWith = null;

        string returnedValue = result.Map(() =>
        {
            successCalled = true;
            return "";
        }, err =>
        {
            errCalledWith = err;
            return funcResult;
        });

        await Assert.That<Error>(errCalledWith).EqualTo(result.Error);
        await Assert.That(successCalled).IsFalse();
        await Assert.That(returnedValue).EqualTo(funcResult);
    }

    [Test]
    public async Task Conversion_FromError__ReturnsInstanceWithError()
    {
        Error err = new("someError");
        Result result = err;

        await Assert.That(result.IsError).IsTrue();
        await Assert.That<Error>(result.Error).EqualTo(err);
    }

    [Test]
    public async Task Conversion_FromBoolTrue__ReturnsInstanceWithSuccess()
    {
        Result result = true;

        await Assert.That(result.IsError).IsFalse();
    }

    [Test]
    public async Task Conversion_FromBoolFalse__ReturnsInstanceWithError()
    {
        Result result = false;

        await Assert.That(result.IsError).IsTrue();
    }

    [Test]
    public async Task Conversion_ToBool__InstanceHasValue__ReturnsTrue()
    {
        Result result = WithSuccess;

        bool value = result;

        await Assert.That(value).IsTrue();
    }

    [Test]
    public async Task Conversion_ToBool__InstanceHasError__ReturnsFalse()
    {
        Result result = WithError;

        bool value = result;

        await Assert.That(value).IsFalse();
    }

    [Test]
    public async Task Conversion_ToError__InstanceHasValue__ReturnsNull()
    {
        Result result = WithSuccess;

        Error? err = result;

        await Assert.That<Error>(err).IsNull();
    }

    [Test]
    public async Task Conversion_ToError__InstanceHasError__ReturnsError()
    {
        Result result = WithError;

        Error? err = result;

        await Assert.That<Error>(err).EqualTo(result.Error);
    }
}
