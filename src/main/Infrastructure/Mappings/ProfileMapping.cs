using JacksonVeroneze.NET.GRPCServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JacksonVeroneze.NET.GRPCServer.Infrastructure.Mappings;

internal sealed class ProfileMapping : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("profile", "profile");

        builder.HasKey(profile => profile.Id)
            .HasName("pk_profile");

        builder.Property(profile => profile.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(profile => profile.FullName)
            .HasColumnName("full_name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(profile => profile.BirthDate)
            .HasColumnName("birth_date")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(profile => profile.Gender)
            .HasColumnName("gender")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(profile => profile.Cpf)
            .HasColumnName("cpf")
            .HasMaxLength(11)
            .IsRequired();

        builder.HasIndex(profile => profile.Cpf)
            .IsUnique()
            .HasDatabaseName("ux_profile_cpf");

        builder.Property(profile => profile.Status)
            .HasColumnName("status")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(profile => profile.Email)
            .HasColumnName("email")
            .HasMaxLength(150);

        builder.Property(profile => profile.PhoneNumber)
            .HasColumnName("phone_number")
            .HasColumnType("bigint")
            .IsRequired();

        builder.Property(profile => profile.MotherName)
            .HasColumnName("mother_name")
            .HasMaxLength(100);

        builder.Property(profile => profile.FatherName)
            .HasColumnName("father_name")
            .HasMaxLength(100);

        builder.Property(profile => profile.Nationality)
            .HasColumnName("nationality")
            .HasMaxLength(60);

        builder.Property(profile => profile.Street)
            .HasColumnName("street")
            .HasMaxLength(150);

        builder.Property(profile => profile.AddressNumber)
            .HasColumnName("address_number")
            .HasColumnType("integer")
            .IsRequired();

        builder.Property(profile => profile.Neighborhood)
            .HasColumnName("neighborhood")
            .HasMaxLength(80);

        builder.Property(profile => profile.City)
            .HasColumnName("city")
            .HasMaxLength(80);

        builder.Property(profile => profile.State)
            .HasColumnName("state")
            .HasMaxLength(40);

        builder.Property(profile => profile.ZipCode)
            .HasColumnName("zip_code")
            .HasMaxLength(20);

        builder.Property(profile => profile.Country)
            .HasColumnName("country")
            .HasMaxLength(60);

        builder.Property(profile => profile.DependentsCount)
            .HasColumnName("dependents_count")
            .HasColumnType("integer")
            .IsRequired();

        builder.Property(profile => profile.IsPoliticallyExposed)
            .HasColumnName("is_politically_exposed")
            .IsRequired();

        builder.Property(profile => profile.IsEmailVerified)
            .HasColumnName("is_email_verified")
            .IsRequired();

        builder.Property(profile => profile.RiskLevel)
            .HasColumnName("risk_level")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(profile => profile.Latitude)
            .HasColumnName("latitude")
            .HasColumnType("double precision")
            .IsRequired();

        builder.Property(profile => profile.Longitude)
            .HasColumnName("longitude")
            .HasColumnType("double precision")
            .IsRequired();

        builder.Property(profile => profile.ActivatedOnUtc)
            .HasColumnName("activated_on_utc");

        builder.Property(profile => profile.InactivatedOnUtc)
            .HasColumnName("inactivated_on_utc");

        builder.Property<int>("Version")
            .HasColumnName("version")
            .IsConcurrencyToken()
            .HasDefaultValue(1);
    }
}
