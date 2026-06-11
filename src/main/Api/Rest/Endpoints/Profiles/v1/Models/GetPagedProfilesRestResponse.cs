namespace JacksonVeroneze.NET.GRPCServer.Api.Rest.Endpoints.Profiles.v1.Models;

internal sealed record GetPagedProfilesRestResponse(
    List<ProfileRestResponse> Data,
    PageInfoRestResponse? Pagination);
