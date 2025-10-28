namespace KestrelsDev.KestrelsCore.ResultPattern;

public record Error(string Message)
{
    public static implicit operator Error(string message) => new(message);
}