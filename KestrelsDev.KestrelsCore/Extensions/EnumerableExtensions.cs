namespace KestrelsDev.KestrelsCore.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>> enumerable)
    {
        List<IEnumerable<T>> list = enumerable.ToList();

        return list.Count != 0
            ? list.Aggregate((a, b) => [..a, ..b])
            : [];
    }

    /// <summary>
    /// Determines whether a sequence contains no elements.
    /// </summary>
    public static bool None<T>(this IEnumerable<T> enumerable) => !enumerable.Any();

    /// <summary>
    /// Determines whether no element of a sequence satisfies a condition.
    /// </summary>
    public static bool None<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate) => !enumerable.Any(predicate);
}