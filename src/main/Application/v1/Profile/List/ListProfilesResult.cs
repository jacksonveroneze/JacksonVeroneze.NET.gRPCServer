using JacksonVeroneze.NET.GRPCServer.Application.Common.Models.Common.Response;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Common.Models;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.List;

public sealed record ListProfilesResult(
    IReadOnlyCollection<ProfileResult> Profiles,
    string? NextPageToken) : PagedResponse<ProfileResult>;
