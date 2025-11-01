// ReSharper disable MemberCanBeProtected.Global

using System.Diagnostics.CodeAnalysis;

namespace KestrelsDev.KestrelsCore.ResultPattern;

// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
public class Result<T> : Result
{
    public T? Value { get; }

    [MemberNotNullWhen(false, nameof(Value))]
    public override bool IsError => base.IsError;

    public virtual bool HasValue => !IsError;

    public Result(T value) : base(null)
    {
        Value = value;
    }

    public Result(Error error) : base(error)
    {
        Value = default;
    }

    public virtual Result<T> Then(Action<T> action)
    {
        if (!IsError)
            action.Invoke(Value);

        return this;
    }

    public override Result<T> Then(Action action) => base.Then(action) as Result<T> ?? this;

    public override Result<T> Catch(Action<Exception> action) => base.Catch(action) as Result<T> ?? this;

    // ReSharper disable once RedundantTypeArgumentsOfMethod
    public override Result<T> Catch<TException>(Action<TException> action) =>
        base.Catch<TException>(action) as Result<T> ?? this;

    public override Result<T> Throw() => base.Throw() as Result<T> ?? this;

    public override Result<T> Throw<TException>() => base.Throw<TException>() as Result<T> ?? this;

    public virtual TNew Map<TNew>(Func<Result<T>, TNew> func) => func.Invoke(this);

    public virtual T Or(T value) => Value ?? value;

    public static implicit operator Result<T>(Error error) => new(error);

    public static implicit operator Result<T>(T value) => new(value);

    public static implicit operator Error?(Result<T> result) => result.Error;

    public static implicit operator T?(Result<T> result) => result.Value;

    public void Deconstruct(out T? value, out Error? error)
    {
        value = Value;
        error = Error;
    }
}

public class Result(Error? error)
{
    public Error? Error => error;

    [MemberNotNullWhen(true, nameof(Error))]
    public virtual bool IsError => Error is not null;

    public virtual Result Then(Action action)
    {
        if (!IsError)
            action.Invoke();

        return this;
    }

    public virtual Result Catch(Action<Exception> action)
    {
        if (IsError)
            action.Invoke(Error.Exception ?? new Exception(Error.Message));

        return this;
    }

    public virtual Result Catch(Action<Error> action)
    {
        if (IsError)
            action.Invoke(Error);

        return this;
    }

    public virtual Result Catch<TException>(Action<TException> action) where TException : Exception
    {
        if (Error?.Exception is TException tEx)
            action.Invoke(tEx);

        return this;
    }

    public virtual Result Catch<TException>(Action<Error> action) where TException : Exception
    {
        if (Error?.Exception is TException tEx)
            action.Invoke(Error);

        return this;
    }

    public virtual Result Throw()
    {
        if (IsError)
            throw Error.Exception ?? new Exception(Error.Message);

        return this;
    }

    public virtual Result Throw<TException>() where TException : Exception
    {
        if (Error?.Exception is TException tEx)
            throw tEx;

        return this;
    }

    public virtual TNew Map<TNew>(Func<Result, TNew> func) => func.Invoke(this);

    public static implicit operator Result(bool success) => new(success ? null : new("Failed without message"));

    public static implicit operator Result(Error error) => new(error);

    public static implicit operator Result(string errorMessage) => new(errorMessage);

    public static implicit operator bool(Result result) => !result.IsError;
}