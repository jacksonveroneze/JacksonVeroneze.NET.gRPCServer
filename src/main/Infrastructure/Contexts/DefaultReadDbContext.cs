using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.GRPCServer.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace JacksonVeroneze.NET.GRPCServer.Infrastructure.Contexts;

[ExcludeFromCodeCoverage]
public class DefaultReadDbContext(
    DbContextOptions<DefaultReadDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(Constants.SchemaName);

        // modelBuilder.ApplyConfiguration(new ShortUrlMapping());
        //
        // modelBuilder.ApplySoftDeleteQueryFilter<ShortUrl>();

        // modelBuilder.IgnoreClass();
    }
}
