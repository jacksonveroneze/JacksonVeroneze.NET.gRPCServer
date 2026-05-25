using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentValidation;

namespace JacksonVeroneze.NET.GRPCServer.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class FluentValidationExtensions
{
    public static IServiceCollection AddFluentValidation(
        this IServiceCollection services, Assembly assembly)
    {
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
