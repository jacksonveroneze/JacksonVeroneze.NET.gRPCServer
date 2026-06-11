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
        builder.HasIndex(c => c.Cpf)
            .IsUnique();

        // Properties
        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.FullName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.BirthDate)
            .IsRequired();

        builder.Property(c => c.Gender)
            .IsRequired();

        builder.Property(c => c.Cpf)
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(c => c.Status)
            .IsRequired();

        builder.Property(c => c.Email)
            .HasMaxLength(150);

        builder.Property(c => c.PhoneNumber)
            .HasMaxLength(30);

        builder.Property(c => c.MotherName)
            .HasMaxLength(100);

        builder.Property(c => c.FatherName)
            .HasMaxLength(100);

        builder.Property(c => c.Nationality)
            .HasMaxLength(60);

        builder.Property(c => c.BirthCity)
            .HasMaxLength(80);

        builder.Property(c => c.BirthState)
            .HasMaxLength(40);

        builder.Property(c => c.Street)
            .HasMaxLength(150);

        builder.Property(c => c.AddressNumber)
            .HasMaxLength(20);

        builder.Property(c => c.Complement)
            .HasMaxLength(80);

        builder.Property(c => c.Neighborhood)
            .HasMaxLength(80);

        builder.Property(c => c.City)
            .HasMaxLength(80);

        builder.Property(c => c.State)
            .HasMaxLength(40);

        builder.Property(c => c.ZipCode)
            .HasMaxLength(20);

        builder.Property(c => c.Country)
            .HasMaxLength(60);

        builder.Property(c => c.ActivatedOnUtc);

        builder.Property(c => c.InactivatedOnUtc);

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
