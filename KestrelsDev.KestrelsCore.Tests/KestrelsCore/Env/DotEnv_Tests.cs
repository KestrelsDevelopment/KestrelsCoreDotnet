using KestrelsDev.KestrelsCore.Env;

namespace KestrelsDev.KestrelsCore.Tests.KestrelsCore.Env;

public class DotEnv_Tests
{
    private string RunPrefix => $"Run_{Random.Shared.Next()}_";

    private string GetEnv(string key) => Environment.GetEnvironmentVariable(key) ?? "";

    [Test]
    [Arguments("key=value", "value")]
    [Arguments("key=", "")]
    [Arguments("key=#value", "")]
    [Arguments("key=value#", "value")]
    [Arguments("key=v#alue", "v")]
    [Arguments("key=v#alue#", "v")]
    [Arguments("key#=value", "")]
    [Arguments("ke#y=value", "")]
    [Arguments("", "")]
    [Arguments("# This is a comment. Set ENVs via key=value", "")]
    [Arguments("key=\"#value\"", "#value")]
    [Arguments("key=\"\"", "")]
    [Arguments("key=\"value\"", "value")]
    [Arguments("key=\"\"value\"\"", "\"value\"")]
    [Arguments("key=\"\"#value\"\"", "\"#value\"")]
    public async Task Load__NotYetSet__ParsesAndSetsEnv(string line, string expectedValue)
    {
        string runPrefix = RunPrefix;
        List<string> lines = [runPrefix + line];

        DotEnv.Load(lines);

        string actual = GetEnv(runPrefix + "key");

        if (actual != expectedValue)
            Assert.Fail($"Expected value \"{expectedValue}\" does not match actual value \"{actual}\"");
    }

    [Test]
    public async Task Load__MultipleLines__ParsesAllLines()
    {
        string runPrefix = RunPrefix;
        List<string> lines = [runPrefix + "key1=value1", runPrefix + "key2=value2", runPrefix + "key3=value3"];

        DotEnv.Load(lines);

        await Assert.That(GetEnv(runPrefix + "key1")).EqualTo("value1");
        await Assert.That(GetEnv(runPrefix + "key2")).EqualTo("value2");
        await Assert.That(GetEnv(runPrefix + "key3")).EqualTo("value3");
    }

    [Test]
    public async Task Load__SomeAlreadySet_NoOverwrite__SetsNewValues_DoesNotOverwriteExisting()
    {
        string runPrefix = RunPrefix;
        List<string> lines = [runPrefix + "key1=value1", runPrefix + "key2=value2", runPrefix + "key3=value3"];

        Environment.SetEnvironmentVariable(runPrefix + "key1", "originalValue");

        DotEnv.Load(lines, false);

        await Assert.That(GetEnv(runPrefix + "key1")).EqualTo("originalValue");
        await Assert.That(GetEnv(runPrefix + "key2")).EqualTo("value2");
        await Assert.That(GetEnv(runPrefix + "key3")).EqualTo("value3");
    }

    [Test]
    public async Task Load__SomeAlreadySet_YesOverwrite__SetsNewValues_OverwritesExisting()
    {
        string runPrefix = RunPrefix;
        List<string> lines = [runPrefix + "key1=value1", runPrefix + "key2=value2", runPrefix + "key3=value3"];

        Environment.SetEnvironmentVariable(runPrefix + "key1", "originalValue");

        DotEnv.Load(lines, true);

        await Assert.That(GetEnv(runPrefix + "key1")).EqualTo("value1");
        await Assert.That(GetEnv(runPrefix + "key2")).EqualTo("value2");
        await Assert.That(GetEnv(runPrefix + "key3")).EqualTo("value3");
    }
}
