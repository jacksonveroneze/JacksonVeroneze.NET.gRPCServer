namespace JacksonVeroneze.NET.GRPCServer.Api.Rest.Endpoints.Profiles.v1.Models;

internal sealed record PageInfoRestResponse(
    int Page,
    int PageSize,
    int TotalPages,
    int TotalElements,
    bool? IsFirstPage,
    bool? IsLastPage,
    bool? HasNextPage,
    bool? HasBackPage,
    int? NextPage,
    int? BackPage);
