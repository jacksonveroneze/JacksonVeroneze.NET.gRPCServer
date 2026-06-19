using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.UseCases;
using JacksonVeroneze.NET.GRPCServer.Domain.Enums;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Create;

public sealed record CreateProfileRequest(
    string FullName,
    DateOnly BirthDate,
    Gender Gender,
    string Cpf,
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
    double Longitude)
    : IBaseRequest;
