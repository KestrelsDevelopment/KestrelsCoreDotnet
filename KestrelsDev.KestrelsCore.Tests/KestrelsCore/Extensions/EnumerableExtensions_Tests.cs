using KestrelsDev.KestrelsCore.Extensions;

namespace KestrelsDev.KestrelsCore.Tests.KestrelsCore.Extensions;

public class EnumerableExtensions_Tests
{
    private bool ContentEquals<T>(IEnumerable<T> first, IEnumerable<T> other)
    {
        if (first.Count() != other.Count())
            return false;

        foreach (var item in first)
            if (!other.Contains(item))
                return false;

        return true;
    }

    [Test]
    public async Task Merge__MultipleInnerLists__ReturnsMergedList()
    {
        List<int> inner1 = [1, 2, 3];
        List<int> inner2 = [4, 5];
        List<List<int>> outer = [inner1, inner2];

        IEnumerable<int> merged = outer.Merge();

        await Assert.That(ContentEquals(merged, [.. inner1, .. inner2])).IsTrue();
    }

    [Test]
    public async Task Merge__EmptyEnclosingList__ReturnsEmpty()
    {
        List<List<int>> outer = [];

        IEnumerable<int> merged = outer.Merge();

        await Assert.That(ContentEquals(merged, [])).IsTrue();
    }

    [Test]
    public async Task Merge__EmptyInnerLists__ReturnsEmpty()
    {
        List<List<int>> outer = [[], []];

        IEnumerable<int> merged = outer.Merge();

        await Assert.That(ContentEquals(merged, [])).IsTrue();
    }

    [Test]
    public async Task Merge__Strings__ReturnsString()
    {
        List<string> list = ["1", "2", "3"];

        string merged = list.Merge();

        await Assert.That(merged).EqualTo("123");
    }

    [Test]
    public async Task AsString__MultipleItems__ReturnsItemsToString()
    {
        List<int> list = [1, 2, 3];

        string merged = list.AsString();

        await Assert.That(merged).EqualTo("123");
    }

    [Test]
    public async Task None__NoPredicate_ListIsEmpty__ReturnsTrue()
    {
        List<int> list = [];

        bool none = list.None();

        await Assert.That(none).IsTrue();
    }

    [Test]
    public async Task None__NoPredicate_ListIsNotEmpty__ReturnsFalse()
    {
        List<int> list = [0, 1, 2];

        bool none = list.None();

        await Assert.That(none).IsFalse();
    }

    [Test]
    public async Task None__Predicate_ListIsEmpty__ReturnsTrue()
    {
        List<int> list = [];

        bool none = list.None(i => i > 0);

        await Assert.That(none).IsTrue();
    }

    [Test]
    public async Task None__Predicate_SomeMatchPredicate__ReturnsFalse()
    {
        List<int> list = [0, 1, 2];

        bool none = list.None(i => i > 0);

        await Assert.That(none).IsFalse();
    }

    [Test]
    public async Task None__Predicate_NoneMatchPredicate__ReturnsTrue()
    {
        List<int> list = [0, 1, 2];

        bool none = list.None(i => i > 10);

        await Assert.That(none).IsTrue();
    }
}
