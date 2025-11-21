using KestrelsDev.KestrelsCore.Extensions;
using Microsoft.EntityFrameworkCore;

namespace KestrelsDev.KestrelsCore.EntityFramework;

public class KestrelsDbContext(DbContextOptions<KestrelsDbContext> options) : DbContext(options)
{
    private readonly Dictionary<string, DbProvider> Providers = new()
    {
        { DbProvider.PostgreSql.Identifier, DbProvider.PostgreSql },
        { DbProvider.Sqlite.Identifier, DbProvider.Sqlite }
    };

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        string? providerIdentifier = Environment.GetEnvironmentVariable("DB_PROVIDER");

        if (providerIdentifier.IsNullOrWhiteSpace())
            throw new ArgumentException("DB_PROVIDER environment variable must be set.");

        foreach (DbProvider dbProvider in AdditionalProviders)
        {
            Providers[dbProvider.Identifier] = dbProvider;
        }

        if(!Providers.TryGetValue(providerIdentifier, out DbProvider? provider))
            throw new ArgumentException($"Database provider \"{providerIdentifier}\" is not supported.");

        string connStr = provider.ConnectionStrFunc.Invoke();
        provider.ConfigurationFunc.Invoke(optionsBuilder, connStr);
    }

    protected virtual IEnumerable<DbProvider> AdditionalProviders => [];
}