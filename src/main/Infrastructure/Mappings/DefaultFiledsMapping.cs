// using System.Diagnostics.CodeAnalysis;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
//
// namespace JacksonVeroneze.NET.GRPCServer.Infrastructure.Mappings;
//
// [ExcludeFromCodeCoverage]
// public static class EntityTypeBuilderExtensions
// {
//     public static void ConfigureEntityDefaultFieldsMapping<TEntity>(
//         this EntityTypeBuilder<TEntity> builder)
//         where TEntity : Entity
//     {
//         ArgumentNullException.ThrowIfNull(builder);
//
//         builder.Property(c => c.CreatedAt)
//             .IsRequired()
//             .HasDefaultValueSql("NOW()");
//
//         builder.Property(c => c.UpdatedAt);
//
//         builder.Property(c => c.DeletedAt);
//
//         builder.Property(c => c.Version)
//             .HasDefaultValue(1)
//             .IsConcurrencyToken()
//             .IsRequired();
//     }
// }
