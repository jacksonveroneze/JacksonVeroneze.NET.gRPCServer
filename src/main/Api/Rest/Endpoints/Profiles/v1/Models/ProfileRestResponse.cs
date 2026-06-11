using JacksonVeroneze.NET.GRPCServer.Domain.Enums;

namespace JacksonVeroneze.NET.GRPCServer.Api.Rest.Endpoints.Profiles.v1.Models;

internal sealed record ProfileRestResponse(
    Guid Id,
    string FullName,
    DateOnly? BirthDate,
    Gender? Gender,
    string? Cpf,
    ProfileStatus Status,
    string? Email,
    string? PhoneNumber,
    string? MotherName,
    string? FatherName,
    string? Nationality,
    string? BirthCity,
    string? BirthState,
    string? Street,
    string? AddressNumber,
    string? Complement,
    string? Neighborhood,
    string? City,
    string? State,
    string? ZipCode,
    string? Country,
    DateTimeOffset? ActivedOnUtc,
    DateTimeOffset? InactivedOnUtc);
