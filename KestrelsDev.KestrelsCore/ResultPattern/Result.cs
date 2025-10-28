using System.Diagnostics.CodeAnalysis;

namespace KestrelsDev.KestrelsCore.ResultPattern;

public class Result<T> : Result
{
    public T? Value { get; }

    [MemberNotNullWhen(false, nameof(Value))]
    public override bool IsError => base.IsError;

    public Result(T value) : base(null)
    {
        Value = value;
    }

    public Result(Error error) : base(error)
    {
        Value = default;
    }

    public static implicit operator Result<T>(Error error) => new(error);

    public static implicit operator Result<T>(T value) => new(value);

    public static implicit operator Error?(Result<T> result) => result.Error;

    public static implicit operator T?(Result<T> result) => result.Value;
}

public class Result(Error? error)
{
    public Error? Error => error;

    // ReSharper disable once MemberCanBeProtected.Global
    [MemberNotNullWhen(true, nameof(Error))]
    public virtual bool IsError => Error is not null;

    public static implicit operator Result(bool success) => new(success ? null : new("Failed without message"));

    public static implicit operator Result(Error error) => new(error);

    public static implicit operator Result(string errorMessage) => new(errorMessage);

    public static implicit operator bool(Result result) => !result.IsError;
}