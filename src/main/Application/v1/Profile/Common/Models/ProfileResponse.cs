using JacksonVeroneze.NET.GRPCServer.Domain.Enums;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Common.Models;

public sealed record ProfileResponse(
    Guid Id,
    string FullName,
    DateOnly? BirthDate,
    Gender? Gender,
    string? Cpf,
    ProfileStatus Status,
    DateTimeOffset? ActivedOnUtc,
    DateTimeOffset? InactivedOnUtc);
