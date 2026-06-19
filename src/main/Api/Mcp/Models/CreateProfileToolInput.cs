using System.ComponentModel;
using JacksonVeroneze.NET.GRPCServer.Domain.Enums;

namespace JacksonVeroneze.NET.GRPCServer.Api.Mcp.Models;

public sealed record CreateProfileToolInput
{
    [Description("Full name of the person. Example: Joana da Silva.")]
    public string? FullName { get; init; }

    [Description("Birth date in yyyy-MM-dd format. Example: 1995-05-25.")]
    public string? BirthDate { get; init; }

    [Description("Gender. Allowed values: Male, Female, Other.")]
    public Gender Gender { get; init; }

    [Description("Brazilian CPF with 11 digits, only numbers. Example: 11122233344.")]
    public string? Cpf { get; init; }

    [Description("Email address. Example: joana.silva@example.com.")]
    public string? Email { get; init; }

    [Description("Phone number with country/area code as digits only. Example: 5511999999999.")]
    public long PhoneNumber { get; init; }

    [Description("Mother full name.")]
    public string? MotherName { get; init; }

    [Description("Father full name.")]
    public string? FatherName { get; init; }

    [Description("Nationality. Example: Brazilian.")]
    public string? Nationality { get; init; }

    [Description("Street address.")]
    public string? Street { get; init; }

    [Description("Address number. Example: 123.")]
    public int AddressNumber { get; init; }

    [Description("Neighborhood.")]
    public string? Neighborhood { get; init; }

    [Description("City.")]
    public string? City { get; init; }

    [Description("State. Example: SP.")]
    public string? State { get; init; }

    [Description("Zip code. Example: 01310930.")]
    public string? ZipCode { get; init; }

    [Description("Country. Example: Brazil.")]
    public string? Country { get; init; }

    [Description("Number of dependents. Example: 2.")]
    public int DependentsCount { get; init; }

    [Description("Indicates whether the person is politically exposed.")]
    public bool IsPoliticallyExposed { get; init; }

    [Description("Indicates whether the email is verified.")]
    public bool IsEmailVerified { get; init; }

    [Description("Risk level. Allowed values: Unspecified, Low, Medium, High, Critical.")]
    public ProfileRiskLevel RiskLevel { get; init; }

    [Description("Latitude. Example: -23.55052.")]
    public double Latitude { get; init; }

    [Description("Longitude. Example: -46.633308.")]
    public double Longitude { get; init; }
}
