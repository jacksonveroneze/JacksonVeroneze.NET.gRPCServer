using JacksonVeroneze.NET.GRPCServer.Domain.Enums;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Common.Models;

public record ProfileResult(
    Guid Id,
    string Name,
    DateOnly? Birthday,
    Gender? Gender,
    string? Cpf,
    ProfileStatus Status,
    DateTimeOffset? ActivedOnUtc,
    DateTimeOffset? InactivedOnUtc);
