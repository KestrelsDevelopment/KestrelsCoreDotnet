using System.Text.RegularExpressions;
using KestrelsDev.KestrelsCore.Extensions;
using KestrelsDev.KestrelsCore.ResultPattern;

namespace KestrelsDev.KestrelsCore.Env;

public static class DotEnv
{
    public static Result Load(string path = ".env", bool overwriteExisting = false)
    {
        path = path.ToLowerInvariant();

        if (!File.Exists(path))
            return false;

        string[] lines = File.ReadAllLines(path);

        foreach (string line in lines)
            LoadLine(line, overwriteExisting);

        return true;
    }

    private static void LoadLine(string line, bool overwriteExisting = false)
    {
        Match match = line.Match("(.*?)=(.*)");

        if (!match.Success || match.Groups.Count < 3)
            return;

        string key = match.Groups[1].Value.Trim();
        string value = match.Groups[2].Value.Trim();

        if(Environment.GetEnvironmentVariable(key).IsNullOrWhiteSpace() && !overwriteExisting)
            return;

        match = value.Match("\"(.+)\".*");

        value = !match.Success || match.Groups.Count < 2
            ? value.Split('#').FirstOrDefault("").Trim()
            : match.Groups[1].Value;

        if (value.IsNullOrWhiteSpace())
            return;

        Environment.SetEnvironmentVariable(key, value);
    }
}