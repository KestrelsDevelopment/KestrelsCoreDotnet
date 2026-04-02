using KestrelsDev.KestrelsCore.Extensions;
using Microsoft.EntityFrameworkCore;

namespace KestrelsDev.KestrelsCore.Web.EntityFramework.Providers;

public class PostgresqlDbProdiver : IDbProvider
{
    public string Identifier => "postgresql";

    public int Priority => 1;

    public bool Configure(DbContextOptionsBuilder options, string? prefix = null)
    {
        string? host = GetEnv(prefix, "POSTGRESQL_HOST");
        string port = GetEnv(prefix, "POSTGRESQL_PORT") ?? "5432";
        string? user = GetEnv(prefix, "POSTGRESQL_USER");
        string? password = GetEnv(prefix, "POSTGRESQL_PASSWORD");
        string? database = GetEnv(prefix, "POSTGRESQL_DATABASE");

        if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(password) ||
            string.IsNullOrWhiteSpace(database))
            return false;

        string connectionStr = $"Host={host}:{port};Username={user};Password={password};Database={database}";

        options.UseNpgsql(connectionStr);

        return true;
    }

    private string? GetEnv(string? prefix, string env)
    {
        string envName = prefix.IsNullOrWhiteSpace() ? env : prefix + env;
        return Environment.GetEnvironmentVariable(envName);
    }
}
