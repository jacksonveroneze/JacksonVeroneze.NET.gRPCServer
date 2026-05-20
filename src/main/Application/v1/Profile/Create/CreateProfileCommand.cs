using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.UseCases;
using JacksonVeroneze.NET.GRPCServer.Domain.Enums;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Create;

public sealed record CreateProfileCommand(
    string Name,
    DateOnly Birthday,
    Gender Gender,
    string Cpf)
    : IBaseRequest;
