namespace KestrelsDev.KestrelsCore;

/// <summary>
/// Provides a collection of constant string values representing various character sets
/// used for manipulation, validation, and categorization in text processing and applications.
/// </summary>
public static class Characters
{
    /// <summary>
    /// Represents a constant string containing all uppercase alphabetic characters
    /// in the English alphabet, ranging from 'A' to 'Z'.
    /// </summary>
    public const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    /// <summary>
    /// Represents a constant string containing all lowercase alphabetical characters
    /// in the English alphabet.
    /// </summary>
    public const string Lowercase = "abcdefghijklmnopqrstuvwxyz";

    /// <summary>
    /// Represents a constant string containing all numeric digits from '0' to '9'.
    /// </summary>
    public const string Digits = "0123456789";

    /// <summary>
    /// Represents a constant string containing all alphanumeric characters,
    /// including uppercase English alphabet letters, lowercase English alphabet letters,
    /// and numeric digits (0-9).
    /// </summary>
    public const string Alphanumeric = Uppercase + Lowercase + Digits;

    /// <summary>
    /// Represents a constant string containing all uppercase English alphabet letters ('A' to 'Z')
    /// and numeric digits ('0' to '9') combined into a single string.
    /// </summary>
    public const string UpperAlphanumeric = Uppercase + Digits;

    /// <summary>
    /// Represents a constant string containing all lowercase English alphabet characters
    /// and numeric digits (0-9).
    /// </summary>
    public const string LowerAlphanumeric = Lowercase + Digits;
}