using JacksonVeroneze.NET.GRPCServer.Api.Rest.Endpoints.Extensions;
using JacksonVeroneze.NET.GRPCServer.Api.Security;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.GetPaged;
using JacksonVeroneze.NET.Result;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.NET.GRPCServer.Api.Rest.Endpoints.Profiles.v1;

internal static class GetPagedProfileEndpoint
{
    public static RouteGroupBuilder AddGetPaged(
        this RouteGroupBuilder builder)
    {
        builder.MapGet(string.Empty, async (
                [FromServices] IGetPagedProfilesUseCase useCase,
                [AsParameters] GetPagedProfilesRequest input,
                CancellationToken cancellationToken) =>
            {
                Result<GetPagedProfilesResponse> output =
                    await useCase.ExecuteAsync(input, cancellationToken);

                return output.ToIResult();
            })
            .Produces<GetPagedProfilesResponse>()
            .AddDefaultResponseEndpoints()
            .RequireAuthorization(AuthorizationPolicies.ProfilesRead);

        return builder;
    }
}
