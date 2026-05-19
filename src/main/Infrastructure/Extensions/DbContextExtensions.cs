// using System.Diagnostics.CodeAnalysis;
// using Microsoft.EntityFrameworkCore;
//
// namespace JacksonVeroneze.NET.GRPCServer.Infrastructure.Extensions;
//
// [ExcludeFromCodeCoverage]
// public static class DbContextExtensions
// {
//     extension(ModelBuilder modelBuilder)
//     {
//         public void ApplySoftDeleteQueryFilter<TEntity>() where TEntity : Entity
//         {
//             modelBuilder.Entity<TEntity>()
//                 .HasQueryFilter(field => field.DeletedAt == null);
//         }
//
//         public void IgnoreClass()
//         {
//             modelBuilder.Ignore<Event>();
//             modelBuilder.Ignore<DomainEvent>();
//             modelBuilder.Ignore<ShortCode>();
//         }
//     }
// }
