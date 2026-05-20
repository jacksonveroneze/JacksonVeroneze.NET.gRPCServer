using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.GRPCServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JacksonVeroneze.NET.GRPCServer.Infrastructure.Mappings;

[ExcludeFromCodeCoverage]
public class ProfileMapping : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("profile");

        // Keys
        builder.HasKey(c => c.Id);

        // Indexes
        builder.HasIndex(c => c.Status);

        // Properties
        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.Name)
            .IsRequired();

        builder.Property(c => c.Birthday)
            .IsRequired();

        builder.Property(c => c.Gender)
            .IsRequired();

        builder.Property(c => c.Cpf)
            .IsRequired();

        builder.Property(c => c.Status)
            .IsRequired();

        builder.Property(c => c.ActivedOnUtc);

        builder.Property(c => c.InactivedOnUtc);
        
        builder.Property(c => c.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        builder.Property(c => c.UpdatedAt);

        builder.Property(c => c.DeletedAt);

        builder.Property(c => c.Version)
            .HasDefaultValue(1)
            .IsConcurrencyToken()
            .IsRequired();
    }
}
