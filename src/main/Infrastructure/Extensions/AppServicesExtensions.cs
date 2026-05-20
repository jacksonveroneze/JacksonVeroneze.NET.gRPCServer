using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.Repositories;
using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.Services;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Activate;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Create;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Inactivate;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.List;
using JacksonVeroneze.NET.GRPCServer.Infrastructure.Repositories.Profile;
using JacksonVeroneze.NET.GRPCServer.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.NET.GRPCServer.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class AppServicesExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IProfileRepository, ProfileRepository>();

        services.AddScoped<ICreateProfileUseCase, CreateProfileUseCase>();
        services.AddScoped<IListProfilesUseCase, ListProfilesUseCase>();
        services.AddScoped<IActivateProfileUseCase, ActivateProfileUseCase>();
        services.AddScoped<IInactivateProfileUseCase, InactivateProfileUseCase>();

        return services;
    }
}
