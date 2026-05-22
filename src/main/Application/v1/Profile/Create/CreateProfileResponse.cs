using JacksonVeroneze.NET.GRPCServer.Application.Common.Models.Common.Response;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Common.Models;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Create;

public sealed record CreateProfileResponse
    : DataResponse<ProfileResponse>;
