using KestrelsDev.KestrelsCore.Extensions;

namespace KestrelsDev.KestrelsCore.Tests.KestrelsCore.Extensions;

public class TaskExtensions_Tests
{
    [Test]
    public async Task Map__AppliesTransformFuncToResult()
    {
        Task<string> task = Task.FromResult("test");
        string? calledWith = null;

        int result = await task.Map(s =>
        {
            calledWith = s;
            return 1;
        });

        await Assert.That(calledWith).EqualTo("test");
        await Assert.That(result).EqualTo(1);
    }
}
