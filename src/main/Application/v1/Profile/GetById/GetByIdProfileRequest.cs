using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.UseCases;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.GetById;

public sealed record GetByIdProfileRequest(Guid Id)
    : IBaseRequest;
