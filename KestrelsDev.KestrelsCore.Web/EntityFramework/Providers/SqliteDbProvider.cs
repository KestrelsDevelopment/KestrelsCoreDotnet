using KestrelsDev.KestrelsCore.Extensions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace KestrelsDev.KestrelsCore.Web.EntityFramework.Providers;

public class SqliteDbProvider : IDbProvider
{
    public string Identifier => "sqlite";

    public int Priority => int.MaxValue;

    public bool Configure(DbContextOptionsBuilder options, string? prefix = null)
    {
        string dataSource = GetEnv(prefix, "SQLITE_DATA_SOURCE") ?? "./database.db";
        string? password = GetEnv(prefix, "SQLITE_PASSWORD");

        string connectionStr = new SqliteConnectionStringBuilder
        {
            DataSource = dataSource,
            Password = password,
            Mode = SqliteOpenMode.ReadWriteCreate,
        }.ToString();

        options.UseSqlite(connectionStr);

        return true;
    }

    private string? GetEnv(string? prefix, string env)
    {
        string envName = prefix.IsNullOrWhiteSpace() ? env : prefix + env;
        return Environment.GetEnvironmentVariable(envName);
    }
}
