using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.UseCases;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Inactivate;

public sealed record InactivateProfileCommand(Guid Id)
    : IBaseRequest;
