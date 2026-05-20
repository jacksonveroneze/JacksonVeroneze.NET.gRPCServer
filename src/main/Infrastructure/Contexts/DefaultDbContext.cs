using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.DomainObjects.Messaging;
using JacksonVeroneze.NET.GRPCServer.Domain.Entities;
using JacksonVeroneze.NET.GRPCServer.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace JacksonVeroneze.NET.GRPCServer.Infrastructure.Contexts;

[ExcludeFromCodeCoverage]
public class DefaultDbContext(
    DbContextOptions<DefaultDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(Constants.SchemaName);

        modelBuilder.ApplyConfiguration(new ProfileMapping());

        modelBuilder.Entity<Profile>()
            .HasQueryFilter(field => field.DeletedAt == null);
        
        modelBuilder.Ignore<Event>();
        modelBuilder.Ignore<DomainEvent>();
    }
}
