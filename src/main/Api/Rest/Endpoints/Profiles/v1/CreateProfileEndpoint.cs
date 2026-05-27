using JacksonVeroneze.NET.GRPCServer.Api.Rest.Endpoints.Extensions;
using JacksonVeroneze.NET.GRPCServer.Api.Security;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Create;
using JacksonVeroneze.NET.Result;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.NET.GRPCServer.Api.Rest.Endpoints.Profiles.v1;

internal static class CreateProfileEndpoint
{
    public static RouteGroupBuilder AddCreate(
        this RouteGroupBuilder builder)
    {
        builder.MapPost(string.Empty, async (
                [FromServices] ICreateProfileUseCase useCase,
                [FromServices] LinkGenerator linkGenerator,
                [FromBody] CreateProfileRequest input,
                HttpContext httpContext,
                CancellationToken cancellationToken) =>
            {
                Result<CreateProfileResponse> output =
                    await useCase.ExecuteAsync(input, cancellationToken);

                return output.ToCreatedResultFromRoute(
                    linkGenerator,
                    httpContext,
                    RouteNames.GetShortUrlById,
                    output.Value?.Data?.Id!
                );
            })
            .Produces<CreateProfileResponse>(
                statusCode: StatusCodes.Status201Created)
            .AddDefaultResponseEndpoints()
            .RequireAuthorization(AuthorizationPolicies.ProfilesCreate);

        return builder;
    }
}
