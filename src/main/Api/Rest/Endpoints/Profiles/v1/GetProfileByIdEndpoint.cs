using JacksonVeroneze.NET.GRPCServer.Api.Rest.Endpoints.Extensions;
using JacksonVeroneze.NET.GRPCServer.Api.Security;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.GetById;
using JacksonVeroneze.NET.Result;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.NET.GRPCServer.Api.Rest.Endpoints.Profiles.v1;

internal static class GetProfileByIdEndpoint
{
    public static RouteGroupBuilder AddGetById(
        this RouteGroupBuilder builder)
    {
        builder.MapGet("{id:guid}", async (
                [FromServices] IGetByIdProfileUseCase useCase,
                Guid id,
                CancellationToken cancellationToken) =>
            {
                GetByIdProfileRequest input = new(id);

                Result<GetByIdProfileResponse> output =
                    await useCase.ExecuteAsync(input, cancellationToken);

                return output.ToIResult();
            })
            .WithName(RouteNames.GetShortUrlById)
            .Produces<GetByIdProfileResponse>()
            .AddDefaultResponseEndpoints()
            .RequireAuthorization(AuthorizationPolicies.ProfilesRead);

        return builder;
    }
}
