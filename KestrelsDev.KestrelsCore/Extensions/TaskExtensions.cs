namespace KestrelsDev.KestrelsCore.Extensions;

public static class TaskExtensions
{
    public static async Task<TNew> Map<T, TNew>(this Task<T> task, Func<T, TNew> transform)
        => transform(await task);
}