using JacksonVeroneze.NET.GRPCServer.Application.Common.Models.Common.Response;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Common.Models;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.GetPaged;

public sealed record GetPagedProfilesResponse
    : PagedResponse<ICollection<ProfileResponse>>;
