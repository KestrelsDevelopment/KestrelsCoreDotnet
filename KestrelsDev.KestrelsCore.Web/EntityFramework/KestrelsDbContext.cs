using KestrelsDev.KestrelsCore.Web.EntityFramework.Providers;
using Microsoft.EntityFrameworkCore;

namespace KestrelsDev.KestrelsCore.Web.EntityFramework;

public abstract class KestrelsDbContext(DbContextOptions<KestrelsDbContext> options) : DbContext(options)
{
    private readonly Dictionary<string, IDbProvider> Providers = [];

    private static Dictionary<Type, IDbProvider> CachedDbProviders = [];

    private void AddDbProvider(IDbProvider provider)
    {
        Providers[provider.Identifier] = provider;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        if (CachedDbProviders.TryGetValue(GetType(), out IDbProvider? provider))
        {
            provider.Configure(optionsBuilder, Prefix);
            return;
        }

        AddDbProvider(new SqliteDbProvider());
        AddDbProvider(new PostgresqlDbProdiver());

        foreach (IDbProvider dbProvider in AdditionalProviders)
            AddDbProvider(dbProvider);

        foreach (IDbProvider dbProvider in Providers.Values.OrderBy(p => p.Priority))
        {
            if (dbProvider.Configure(optionsBuilder, Prefix))
            {
                CachedDbProviders[GetType()] = dbProvider;
                return;
            }
        }

        throw new InvalidOperationException($"Failed to configure a database provider (tried {Providers.Count})");
    }

    protected virtual IEnumerable<IDbProvider> AdditionalProviders => [];

    protected virtual string Prefix => "";
}