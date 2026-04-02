using KestrelsDev.KestrelsCore.Extensions;

namespace KestrelsDev.KestrelsCore.Tests.KestrelsCore.Extensions;

public class StringExtensions_Tests
{
    [Test]
    [Arguments("test", "test", true)]
    [Arguments("test", "Test", true)]
    [Arguments("Test", "test", true)]
    [Arguments("test", "other", false)]
    public async Task EqualsIgnoreCase__ComparesStrings(string str1, string str2, bool compareResult)
    {
        bool result = str1.EqualsIgnoreCase(str2);

        await Assert.That(result).EqualTo(compareResult);
    }

    [Test]
    [Arguments("", true)]
    [Arguments(null, true)]
    [Arguments(" ", false)]
    [Arguments("\t", false)]
    [Arguments("\n", false)]
    [Arguments("test", false)]
    public async Task IsNullOrEmpty__ReturnsTrueIfStringIsNullOrEmpty(string? str, bool expected)
    {
        bool result = str.IsNullOrEmpty();

        await Assert.That(result).EqualTo(expected);
    }

    [Test]
    [Arguments("", true)]
    [Arguments(null, true)]
    [Arguments(" ", true)]
    [Arguments("\t", true)]
    [Arguments("\n", true)]
    [Arguments("test", false)]
    public async Task IsNullOrWhiteSpace__ReturnsTrueIfStringIsNullOrWhitespace(string? str, bool expected)
    {
        bool result = str.IsNullOrWhiteSpace();

        await Assert.That(result).EqualTo(expected);
    }
}
