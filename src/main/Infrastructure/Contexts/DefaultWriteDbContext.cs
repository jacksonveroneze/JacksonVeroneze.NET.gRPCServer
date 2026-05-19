using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.GRPCServer.Infrastructure.Extensions;
using JacksonVeroneze.NET.GRPCServer.Infrastructure.Mappings.Domain;
using Microsoft.EntityFrameworkCore;

namespace JacksonVeroneze.NET.GRPCServer.Infrastructure.Contexts;

[ExcludeFromCodeCoverage]
public class DefaultWriteDbContext(
    DbContextOptions<DefaultWriteDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(Constants.SchemaName);

        modelBuilder.ApplyConfiguration(new ShortUrlMapping());

        // modelBuilder.ApplySoftDeleteQueryFilter<ShortUrl>();

        // modelBuilder.IgnoreClass();
    }
}
