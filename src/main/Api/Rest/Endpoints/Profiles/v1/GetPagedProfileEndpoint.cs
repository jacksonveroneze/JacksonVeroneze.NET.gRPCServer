using JacksonVeroneze.NET.GRPCServer.Api.Rest.Endpoints.Extensions;
using JacksonVeroneze.NET.GRPCServer.Api.Rest.Endpoints.Profiles.v1.Models;
using JacksonVeroneze.NET.GRPCServer.Api.Security;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.GetPaged;
using JacksonVeroneze.NET.Result;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.NET.GRPCServer.Api.Rest.Endpoints.Profiles.v1;

internal static class GetPagedProfileEndpoint
{
    public static RouteGroupBuilder AddGetPaged(
        this RouteGroupBuilder builder)
    {
        builder.MapGet(string.Empty, async (
                [FromServices] IMapper mapper,
                [FromServices] IGetPagedProfilesUseCase useCase,
                [AsParameters] GetPagedProfilesRestRequest request,
                CancellationToken cancellationToken) =>
            {
                var input = mapper.Map<GetPagedProfilesRestRequest,
                    GetPagedProfilesRequest>(request);

                Result<GetPagedProfilesResponse> output =
                    await useCase.ExecuteAsync(input, cancellationToken);

                var response = mapper.Map<GetPagedProfilesResponse, 
                    GetPagedProfilesRestResponse>(output.Value!);
                
                return Results.Ok(response);
            })
            .Produces<GetPagedProfilesRestResponse>()
            .AddDefaultResponseEndpoints()
            .RequireAuthorization(AuthorizationPolicies.ProfilesRead);

        return builder;
    }
}
