// ReSharper disable MemberCanBeProtected.Global

using System.Diagnostics.CodeAnalysis;

namespace KestrelsDev.KestrelsCore.ResultPattern;

// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
/// <summary>
/// Represents the result of an operation that can either hold a value of type <typeparamref name="T"/> or indicate an error.
/// Provides methods to handle, transform, or retrieve the result's value.
/// </summary>
/// <typeparam name="T">The type of the value contained in the result when successful.</typeparam>
public class Result<T> : Result
{
    /// <summary>
    /// Gets the value contained within the result.
    /// </summary>
    /// <remarks>
    /// This property provides access to the value if the operation was successful. If the result represents
    /// an error, this property will return the default value for the type <typeparamref name="T"/>.
    /// To verify if a valid value exists, use the <see cref="HasValue"/> property.
    /// </remarks>
    public T? Value { get; }

    /// <summary>
    /// Indicates whether the result represents an error state.
    /// </summary>
    /// <remarks>
    /// This property returns true if the result contains an error, otherwise false.
    /// It is used to check whether the operation associated with the result was unsuccessful.
    /// If true, the underlying error can be inspected using the <see cref="Error"/> property.
    /// </remarks>
    [MemberNotNullWhen(false, nameof(Value))]
    public override bool IsError => base.IsError;

    /// <summary>
    /// Indicates whether the result contains a valid value.
    /// </summary>
    /// <remarks>
    /// This property returns true when the result holds a valid value, meaning the operation was successful.
    /// If the operation resulted in an error or does not contain a valid value, this property returns false.
    /// </remarks>
    public virtual bool HasValue => !IsError;

    /// <summary>
    /// Represents the outcome of an operation, encapsulating state that can indicate success or failure.
    /// Enables fluent propagation, error handling, and transformation of result information.
    /// </summary>
    public Result(T value) : base(null)
    {
        Value = value;
    }

    /// <summary>
    /// Represents the result of an operation, encapsulating a potential success or error state.
    /// Facilitates handling, validation, and chaining of operations to enable streamlined workflows.
    /// </summary>
    public Result(Error error) : base(error)
    {
        Value = default;
    }

    /// <summary>
    /// Executes an action if the result of the operation is successful and contains a value.
    /// Enables chaining operations to perform additional logic when no error has occurred.
    /// </summary>
    /// <param name="action">The action to execute, which takes the successful result's value as its argument.</param>
    /// <returns>Returns the current result instance for method chaining.</returns>
    public virtual Result<T> Then(Action<T> action)
    {
        if (!IsError)
            action.Invoke(Value);

        return this;
    }

    /// <summary>
    /// Executes the provided action if the result is successful and not an error.
    /// Enables chaining of operations that depend on the success status of the result.
    /// </summary>
    /// <param name="action">The action to invoke when the result is successful.</param>
    /// <returns>The current result instance for continuation of result handling operations.</returns>
    public override Result<T> Then(Action action) => base.Then(action) as Result<T> ?? this;

    /// <summary>
    /// Executes the specified action if the current <see cref="Result{T}"/> or its base representation
    /// contains an error with an associated exception. If no exception is explicitly provided, a new exception
    /// is created and passed to the action based on the error message.
    /// </summary>
    /// <param name="action">The action to execute, which takes the exception as a parameter.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> instance, enabling further chaining of operations.
    /// If the instance is not in an error state, the action is not invoked, and the original instance is returned.
    /// </returns>
    public override Result<T> Catch(Action<Exception> action) => base.Catch(action) as Result<T> ?? this;

    // ReSharper disable once RedundantTypeArgumentsOfMethod
    /// <summary>
    /// Executes the specified action if an error is present in the current result.
    /// </summary>
    /// <param name="action">The action to execute, which receives the exception causing the error as its parameter.</param>
    /// <returns>The current <see cref="Result"/> instance for further chaining of operations.</returns>
    public override Result<T> Catch<TException>(Action<TException> action) =>
        base.Catch<TException>(action) as Result<T> ?? this;

    /// <summary>
    /// Throws the encapsulated exception if the result represents an error.
    /// If the error does not contain an exception, a generic exception with the error message is thrown.
    /// </summary>
    /// <returns>
    /// The current instance of <see cref="Result"/> if no exception is thrown.
    /// </returns>
    public override Result<T> Throw() => base.Throw() as Result<T> ?? this;

    /// <summary>
    /// Throws the contained exception if the result represents an error.
    /// If no specific exception is contained, a generic exception with the error message is thrown.
    /// </summary>
    /// <returns>
    /// The current <see cref="Result"/> instance if the result does not represent an error or after the exception is thrown.
    /// </returns>
    public override Result<T> Throw<TException>() => base.Throw<TException>() as Result<T> ?? this;

    /// <summary>
    /// Applies a mapping function to the current <see cref="Result{T}"/> instance, transforming it into a new value of type <typeparamref name="TNew"/>.
    /// </summary>
    /// <typeparam name="TNew">The type of the value resulting from the mapping function.</typeparam>
    /// <param name="func">The function used to transform the current <see cref="Result{T}"/> instance into a value of type <typeparamref name="TNew"/>.</param>
    /// <returns>A new value of type <typeparamref name="TNew"/> derived from the mapping function.</returns>
    public virtual TNew Map<TNew>(Func<Result<T>, TNew> func) => func.Invoke(this);

    /// <summary>
    /// Returns the value contained in the result if available; otherwise, returns the provided fallback value.
    /// </summary>
    /// <param name="value">The fallback value to return if the result does not contain a value.</param>
    /// <returns>The value contained in the result if present; otherwise, the provided fallback value.</returns>
    public virtual T Or(T value) => Value ?? value;

    /// <summary>
    /// Defines a custom operator for a specific type or operation. Operators enable
    /// the convenient use of Result objects through implicit or explicit conversions,
    /// simplifying error handling and value manipulation.
    /// </summary>
    public static implicit operator Result<T>(Error error) => new(error);

    /// <summary>
    /// Defines a custom operator to enable specific behaviors or operations on objects of this type.
    /// Facilitates enhanced syntax, improving code readability and usability for supported operations.
    /// </summary>
    public static implicit operator Result<T>(T value) => new(value);

    /// <summary>
    /// Defines a custom operator for a type. This operator allows implicit conversion from a result instance
    /// of a given type to an <see cref="Error"/> type, facilitating seamless access to error details when
    /// an operation result represents a failure.
    /// </summary>
    public static implicit operator Error?(Result<T> result) => result.Error;

    /// <summary>
    /// Defines a custom conversion operator allowing a <see cref="Result{T}"/> to be implicitly cast to its contained value of type <typeparamref name="T"/>.
    /// This provides a convenient way to extract the value when the result is successful, while abstracting the error handling.
    /// </summary>
    public static implicit operator T?(Result<T> result) => result.Value;

    /// <summary>
    /// Deconstructs the result into its value and error components.
    /// </summary>
    /// <param name="value">An output parameter that will contain the value if the result is successful; otherwise, null.</param>
    /// <param name="error">An output parameter that will contain the error if the result is unsuccessful; otherwise, null.</param>
    public void Deconstruct(out T? value, out Error? error)
    {
        value = Value;
        error = Error;
    }
}

/// <summary>
/// Represents the outcome of an operation, potentially encapsulating an error if the operation fails or indicating success otherwise.
/// </summary>
public class Result(Error? error)
{
    /// <summary>
    /// Gets the associated error for the current result, if an error exists.
    /// </summary>
    /// <remarks>
    /// This property provides details about the error that occurred during the operation. It can include
    /// an error message, exception, or additional contextual information encapsulated in the error object.
    /// If the operation was successful, this property will return null.
    /// </remarks>
    /// <value>
    /// An instance of <see cref="Error"/> providing information about the failure, or null if the result indicates success.
    /// </value>
    public Error? Error => error;

    /// <summary>
    /// Gets a value indicating whether the result represents an error.
    /// </summary>
    /// <remarks>
    /// This property returns <c>true</c> if the operation resulted in an error,
    /// and <c>false</c> if the operation was successful. When this property
    /// returns <c>true</c>, the <see cref="Error"/> property contains the
    /// associated error details.
    /// </remarks>
    [MemberNotNullWhen(true, nameof(Error))]
    public virtual bool IsError => Error is not null;

    /// <summary>
    /// Executes the provided action if the current result does not represent an error.
    /// Returns the current result instance for further chaining.
    /// </summary>
    /// <param name="action">The action to invoke if the result is not an error.</param>
    /// <returns>The current result instance for further chaining.</returns>
    public virtual Result Then(Action action)
    {
        if (!IsError)
            action.Invoke();

        return this;
    }

    /// <summary>
    /// Executes the specified action if the current <see cref="Result{T}"/> represents an error,
    /// and the error contains an exception. Allows for custom exception handling logic.
    /// </summary>
    /// <param name="action">The action to execute, which receives the exception causing the error (if present) or a new exception using the error message as input.</param>
    /// <returns>
    /// The current <see cref="Result{T}"/> instance. If the result is not an error, the <paramref name="action"/> is not executed, and the object is returned unchanged.
    /// </returns>
    public virtual Result Catch(Action<Exception> action)
    {
        if (IsError)
            action.Invoke(Error.Exception ?? new Exception(Error.Message));

        return this;
    }

    /// <summary>
    /// Executes the specified action if the result represents an error.
    /// </summary>
    /// <param name="action">The action to execute, which receives the encapsulated error details as its argument.</param>
    /// <returns>The current <see cref="Result"/> instance, allowing for further chained operations.</returns>
    public virtual Result Catch(Action<Error> action)
    {
        if (IsError)
            action.Invoke(Error);

        return this;
    }

    /// <summary>
    /// Catches and processes an exception of the specified type if the result contains an associated error with such an exception.
    /// </summary>
    /// <typeparam name="TException">The type of the exception to catch. Must derive from <see cref="Exception"/>.</typeparam>
    /// <param name="action">An action to execute when the error contains an exception of type <typeparamref name="TException"/>.
    /// The exception is passed as a parameter to the provided action.</param>
    /// <returns>The current result instance, allowing for method chaining.</returns>
    public virtual Result Catch<TException>(Action<TException> action) where TException : Exception
    {
        if (Error?.Exception is TException tEx)
            action.Invoke(tEx);

        return this;
    }

    /// <summary>
    /// Handles errors or specific exceptions in the result by executing the provided action.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be caught and handled.</typeparam>
    /// <param name="action">
    /// A delegate to be executed when the result contains an error caused by the specified exception type.
    /// The delegate receives the associated <see cref="Error"/> instance.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="Result"/> to enable method chaining.
    /// </returns>
    public virtual Result Catch<TException>(Action<Error> action) where TException : Exception
    {
        if (Error?.Exception is TException)
            action.Invoke(Error);

        return this;
    }

    /// <summary>
    /// Throws the encapsulated exception if the result represents an error.
    /// If the error does not contain an exception, a generic exception with the error message is thrown.
    /// </summary>
    /// <returns>
    /// The current result instance if no exception is thrown.
    /// </returns>
    public virtual Result Throw()
    {
        if (IsError)
            throw Error.Exception ?? new Exception(Error.Message);

        return this;
    }

    /// <summary>
    /// Throws the exception encapsulated in the result's error if the result represents an error state.
    /// If no exception is present, the method does nothing and proceeds normally.
    /// </summary>
    /// <returns>
    /// The current instance of the <see cref="Result"/> if the result does not indicate an error.
    /// If the result indicates an error and contains an exception, that exception is thrown.
    /// </returns>
    public virtual Result Throw<TException>() where TException : Exception
    {
        if (Error?.Exception is TException tEx)
            throw tEx;

        return this;
    }

    /// <summary>
    /// Maps the current <see cref="Result"/> instance to a new value of type <typeparamref name="TNew"/>
    /// using the specified mapping function.
    /// </summary>
    /// <typeparam name="TNew">The type to which the current <see cref="Result"/> instance will be mapped.</typeparam>
    /// <param name="func">The mapping function that takes the current <see cref="Result"/> instance and returns a value of type <typeparamref name="TNew"/>.</param>
    /// <returns>A value of type <typeparamref name="TNew"/> produced by applying the mapping function to the current <see cref="Result"/> instance.</returns>
    public virtual TNew Map<TNew>(Func<Result, TNew> func) => func.Invoke(this);

    /// <summary>
    /// Defines a custom operator for the Result class, enabling implicit conversion
    /// from a boolean value to a Result object. This allows direct assignment of a boolean
    /// to a Result instance, simplifying success or failure representation in code.
    /// </summary>
    public static implicit operator Result(bool success) => new(success ? null : new("Failed without message"));

    /// <summary>
    /// Defines a custom operator to enable implicit conversion from an <see cref="Error"/> instance
    /// to a <see cref="Result"/>, facilitating seamless creation of a Result object based on error information.
    /// Enhances flexibility and improves code clarity by reducing explicit instantiation requirements.
    /// </summary>
    public static implicit operator Result(Error error) => new(error);

    /// <summary>
    /// Defines a custom operator for a type, enabling conversion from a string to a <see cref="Result"/>.
    /// This operator provides a way to create a <see cref="Result"/> object directly from an error message,
    /// encapsulating the message in an error instance. This simplifies error handling and construction of <see cref="Result"/> objects.
    /// </summary>
    public static implicit operator Result(string errorMessage) => new(errorMessage);

    /// <summary>
    /// Defines a custom operator for the class or struct, enabling the implementation
    /// of specific operations tailored for the type. This allows seamless behavior
    /// like conversions or interactions that enhance code clarity and expressiveness.
    /// </summary>
    public static implicit operator bool(Result result) => !result.IsError;

    /// <summary>
    /// Executes the given action and encapsulates its outcome in a <see cref="Result"/>.
    /// On successful execution, a successful <see cref="Result"/> is returned.
    /// If an exception is thrown during execution, it captures the exception details in an <see cref="Error"/> within the <see cref="Result"/>.
    /// </summary>
    /// <param name="action">The action to be executed, representing the operation to perform.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the operation.
    /// If an exception is thrown, the returned <see cref="Result"/> contains an <see cref="Error"/> encapsulating the exception information.</returns>
    public static Result From(Action action)
    {
        try
        {
            action.Invoke();

            return true;
        }
        catch (Exception e)
        {
            return (Error)e;
        }
    }

    /// <summary>
    /// Creates a Result by asynchronously executing the specified function and handling any exceptions thrown during its execution.
    /// Converts the success or failure of the function into a Result object.
    /// </summary>
    /// <param name="action">The asynchronous function to execute. If the function completes successfully, the method returns a success Result. If an exception is thrown, an Error Result is returned.</param>
    /// <returns>An asynchronous Task containing a Result. The Result represents success if the function completes without exceptions, or an Error if an exception is encountered.</returns>
    public static async Task<Result> From(Func<Task> action)
    {
        try
        {
            await action.Invoke();

            return true;
        }
        catch (Exception e)
        {
            return (Error)e;
        }
    }

    /// <summary>
    /// Executes a function that produces a result of type <typeparamref name="T"/> and wraps the execution
    /// in a <see cref="Result{T}"/> object, handling any exceptions that may occur during invocation.
    /// </summary>
    /// <typeparam name="T">The type of the result produced by the function.</typeparam>
    /// <param name="action">A function that returns a value of type <typeparamref name="T"/>.</param>
    /// <returns>A <see cref="Result{T}"/> containing the value produced by the function if successful,
    /// or an error encapsulating any exception that occurs.</returns>
    public static Result<T> From<T>(Func<T> action)
    {
        try
        {
            return action.Invoke();
        }
        catch (Exception e)
        {
            return (Error)e;
        }
    }

    /// <summary>
    /// Creates a <see cref="Result"/> that represents the outcome of an operation.
    /// Captures any exception encountered during execution as an error.
    /// </summary>
    /// <param name="action">The operation to execute.</param>
    /// <returns>
    /// A <see cref="Result"/> indicating success if the operation completes without exception,
    /// or an error if an exception is thrown.
    /// </returns>
    public static async Task<Result<T>> From<T>(Func<Task<T>> action)
    {
        try
        {
            return await action.Invoke();
        }
        catch (Exception e)
        {
            return (Error)e;
        }
    }
}