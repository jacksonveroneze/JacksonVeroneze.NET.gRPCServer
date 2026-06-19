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
    long PhoneNumber,
    string? MotherName,
    string? FatherName,
    string? Nationality,
    string? Street,
    int AddressNumber,
    string? Neighborhood,
    string? City,
    string? State,
    string? ZipCode,
    string? Country,
    int DependentsCount,
    bool IsPoliticallyExposed,
    bool IsEmailVerified,
    ProfileRiskLevel RiskLevel,
    double Latitude,
    double Longitude,
    DateTimeOffset? ActivedOnUtc,
    DateTimeOffset? InactivedOnUtc);
