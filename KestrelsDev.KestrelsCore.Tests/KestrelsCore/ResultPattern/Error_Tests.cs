using KestrelsDev.KestrelsCore.ResultPattern;

namespace KestrelsDev.KestrelsCore.Tests.KestrelsCore.ResultPattern;

public class Error_Tests
{
    [Test]
    public async Task IsSimilarTo__SameErrorMessage__ReturnsTrue()
    {
        string errorMessage = "error";
        Error first = new(errorMessage);
        Error second = new(errorMessage);

        bool similar = first.IsSimilarTo(second);

        await Assert.That(similar).IsTrue();
    }

    [Test]
    public async Task IsSimilarTo__DifferentErrorMessage__ReturnsFalse()
    {
        Error first = new("1st");
        Error second = new("2nd");

        bool similar = first.IsSimilarTo(second);

        await Assert.That(similar).IsFalse();
    }
}
