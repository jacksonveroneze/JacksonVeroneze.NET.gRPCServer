using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.GRPCServer.Infrastructure.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace JacksonVeroneze.NET.GRPCServer.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthentication(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(appConfiguration);

        services.AddAuthentication(
                JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = appConfiguration.Auth!.Authority;
                options.Audience = appConfiguration.Auth.Audience;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                    };
            });

        return services;
    }
}
