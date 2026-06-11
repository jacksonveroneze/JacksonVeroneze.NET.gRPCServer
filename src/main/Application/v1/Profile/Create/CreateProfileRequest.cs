using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.UseCases;
using JacksonVeroneze.NET.GRPCServer.Domain.Enums;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Create;

public sealed record CreateProfileRequest(
    string FullName,
    DateOnly BirthDate,
    Gender Gender,
    string Cpf,
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
    string? Country)
    : IBaseRequest;
