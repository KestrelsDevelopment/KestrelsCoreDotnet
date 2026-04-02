using KestrelsDev.KestrelsCore.Extensions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace KestrelsDev.KestrelsCore.Web.EntityFramework.Providers;

public interface IDbProvider
{
    /// <summary>
    /// The identifier of this database provider. Used to override preconfigured providers.
    /// </summary>
    public string Identifier { get; }

    /// <summary>
    /// The priority of this database providers. All configured providers are attempted to be used in order of ascending priority.
    /// </summary>
    public int Priority { get; }

    /// <summary>
    /// The method used to configure the database provider.
    /// It must leave the <see cref="DbContext"/> configured by this <see cref="DbContextOptionsBuilder"/> 
    /// in a valid state to establish a database connection
    /// </summary>
    /// <param name="options">The <see cref="DbContextOptionsBuilder"/> to configure.</param>
    /// <param name="prefix">The name of the <see cref="DbContext"/> to configure</param>
    /// <returns>true if configuration was successful, otherwise false.</returns>
    public bool Configure(DbContextOptionsBuilder options, string? prefix = null);
}