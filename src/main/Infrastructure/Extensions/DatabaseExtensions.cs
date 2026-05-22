using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.EntityFramework.Extensions;
using JacksonVeroneze.NET.GRPCServer.Infrastructure.Configurations;
using JacksonVeroneze.NET.GRPCServer.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.NET.GRPCServer.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class DatabaseExtensions
{
    private const int DefaultCommandTimeout = 5;

    extension(IServiceCollection services)
    {
        public IServiceCollection AddDatabase(
            AppConfiguration appConfiguration)
        {
            ArgumentNullException.ThrowIfNull(appConfiguration);

            services.AddRepository()
                .InternalAddDatabase<DefaultDbContext>(
                    appConfiguration.Database!.ConnectionString!, useInMemory: false);

            return services;
        }

        private IServiceCollection InternalAddDatabase<TContext>(
            string connectionString,
            QueryTrackingBehavior behavior = QueryTrackingBehavior.TrackAll,
            bool useInMemory = false)
            where TContext : DbContext
        {
            ArgumentException.ThrowIfNullOrEmpty(connectionString);

            if (useInMemory)
            {
                services.AddDbContext<TContext>((_, options) =>
                    options.UseNpgsql(connectionString, conf =>
                        {
                            conf.EnableRetryOnFailure()
                                .CommandTimeout(DefaultCommandTimeout);
                        })
                        .UseQueryTrackingBehavior(behavior)
                        .ConfigureOptionsDatabase());
            }
            else
            {
                services.AddDbContext<TContext>((_, options) =>
                    options.UseInMemoryDatabase("db_name")
                        .UseQueryTrackingBehavior(behavior)
                        .ConfigureOptionsDatabase());
            }

            return services;
        }
    }

    private static void ConfigureOptionsDatabase(
        this DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .EnableThreadSafetyChecks()
            .UseSnakeCaseNamingConvention();
    }
}
