using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.GRPCServer.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JacksonVeroneze.NET.GRPCServer.Infrastructure.Mappings.Domain;

[ExcludeFromCodeCoverage]
public class ShortUrlMapping : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("url");

        // // Keys
        // builder.HasKey(c => c.Code);
        //
        // builder.Property(c => c.Code)
        //     .HasColumnName("code")
        //     .HasMaxLength(100)
        //     .IsRequired()
        //     .ValueGeneratedNever();
        //
        // builder.Property(c => c.OriginalUrl)
        //     .HasColumnName("original_url")
        //     .HasMaxLength(1_000)
        //     .IsRequired();
        //
        // builder.Property(c => c.ExpiresAt)
        //     .HasColumnName("expires_at")
        //     .IsRequired();
        //
        // builder.ConfigureEntityDefaultFieldsMapping();
    }
}
