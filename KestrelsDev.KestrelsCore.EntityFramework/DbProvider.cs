using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace KestrelsDev.KestrelsCore.EntityFramework;

public record DbProvider(string Identifier, Func<string> ConnectionStrFunc, Action<DbContextOptionsBuilder, string> ConfigurationFunc)
{
    public static readonly DbProvider PostgreSql = new(
        "postgresql",
        () =>
        {
            string? host = Environment.GetEnvironmentVariable("DB_HOST");
            string port = Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
            string? user = Environment.GetEnvironmentVariable("DB_USER");
            string? password = Environment.GetEnvironmentVariable("DB_PASSWORD");
            string? database = Environment.GetEnvironmentVariable("DB_DATABASE");

            if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(database))
                throw new ArgumentException(
                    "The environment variables DB_HOST, DB_USER, DB_PASSWORD and DB_DATABASE must be set when using this provider.");

            string connectionStr = $"Host={host}:{port};Username={user};Password={password};Database={database}";

            return connectionStr;
        },
        (options, connStr) => options.UseNpgsql(connStr));

    public static readonly DbProvider Sqlite = new(
        "sqlite",
        () =>
        {
            string? dataSource = Environment.GetEnvironmentVariable("DB_DATA_SOURCE");
            string? user = Environment.GetEnvironmentVariable("DB_USER");
            string? password = Environment.GetEnvironmentVariable("DB_PASSWORD");

            if (string.IsNullOrWhiteSpace(dataSource) || string.IsNullOrWhiteSpace(user))
                throw new ArgumentException(
                    "The environment variables DB_DATA_SOURCE and DB_USER must be set when using this provider.");

            string connectionStr = new SqliteConnectionStringBuilder
            {
                DataSource = dataSource,
                Password = password,
                Mode = SqliteOpenMode.ReadWriteCreate,
            }.ToString();

            return connectionStr;
        },
        (options, connStr) => options.UseSqlite(connStr));
}