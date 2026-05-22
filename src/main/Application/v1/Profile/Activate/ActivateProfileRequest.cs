using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.UseCases;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Activate;

public sealed record ActivateProfileRequest(Guid Id)
    : IBaseRequest;
