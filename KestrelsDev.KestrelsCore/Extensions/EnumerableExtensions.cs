namespace KestrelsDev.KestrelsCore.Extensions;

/// <summary>
/// Provides a set of static methods for extended operations on objects implementing the IEnumerable interface.
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// Merges multiple sequences into a single sequence.
    /// </summary>
    /// <param name="enumerable">A collection of sequences to merge.</param>
    /// <typeparam name="T">The type of elements in the sequences.</typeparam>
    /// <returns>A single sequence containing all elements from the input sequences, in order.</returns>
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
    /// <param name="enumerable">The sequence to check for emptiness.</param>
    /// <returns>
    /// true if the sequence contains no elements; otherwise, false.
    /// </returns>
    public static bool None<T>(this IEnumerable<T> enumerable) => !enumerable.Any();

    /// <summary>
    /// Determines whether no elements in the sequence satisfy the specified condition.
    /// </summary>
    /// <param name="enumerable">The sequence of elements to test.</param>
    /// <param name="predicate">The function that defines the condition each element must satisfy.</param>
    /// <returns>
    /// True if no elements in the sequence satisfy the condition defined by the predicate; otherwise, false.
    /// </returns>
    public static bool None<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate) => !enumerable.Any(predicate);
}