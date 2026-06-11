using System.ComponentModel;
using JacksonVeroneze.NET.GRPCServer.Domain.Enums;

namespace JacksonVeroneze.NET.GRPCServer.Api.Mcp.Models;

public sealed record CreateProfileToolInput
{
    [Description("Full name of the person. Example: Joana da Silva.")]
    public string? FullName { get; init; }

    [Description("Birth date in yyyy-MM-dd format. Example: 1995-05-25.")]
    public string? BirthDate { get; init; }

    [Description("Gender. Allowed values: Male, Female, Other, NotInformed.")]
    public Gender Gender { get; init; }

    [Description("Brazilian CPF with 11 digits, only numbers. Example: 11122233344.")]
    public string? Cpf { get; init; }

    [Description("Email address. Example: joana.silva@example.com.")]
    public string? Email { get; init; }

    [Description("Phone number. Example: +55 11 99999-9999.")]
    public string? PhoneNumber { get; init; }

    [Description("Mother full name.")]
    public string? MotherName { get; init; }

    [Description("Father full name.")]
    public string? FatherName { get; init; }

    [Description("Nationality. Example: Brazilian.")]
    public string? Nationality { get; init; }

    [Description("Birth city.")]
    public string? BirthCity { get; init; }

    [Description("Birth state.")]
    public string? BirthState { get; init; }

    [Description("Street address.")]
    public string? Street { get; init; }

    [Description("Address number.")]
    public string? AddressNumber { get; init; }

    [Description("Address complement.")]
    public string? Complement { get; init; }

    [Description("Neighborhood.")]
    public string? Neighborhood { get; init; }

    [Description("City.")]
    public string? City { get; init; }

    [Description("State.")]
    public string? State { get; init; }

    [Description("Zip code.")]
    public string? ZipCode { get; init; }

    [Description("Country.")]
    public string? Country { get; init; }

    [Description("Monthly income. Example: 8500.50.")]
    public decimal MonthlyIncome { get; init; }

    [Description("Number of dependents. Example: 2.")]
    public int DependentsCount { get; init; }

    [Description("Indicates whether the person is politically exposed.")]
    public bool IsPoliticallyExposed { get; init; }

    [Description("Credit score. Example: 750.25.")]
    public double CreditScore { get; init; }

    [Description("Last login date in ISO 8601 format. Example: 2026-06-11T10:30:00Z.")]
    public string? LastLoginAtUtc { get; init; }
}
